import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { User } from '../models/User';
import {BehaviorSubject} from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl=environment.apiUrl;

  behavior:BehaviorSubject<User | null>=new BehaviorSubject<User | null> (null) ;

  userstatus$=this.behavior.asObservable();


  constructor(private HttpService:HttpClient) { }

  login(model:any)
  {
    let _model=this.HttpService.post<User>(this.baseUrl+'Account/login',model).pipe(
      map((user:User)=>{
        // console.log(user);
        localStorage.setItem('user',JSON.stringify(user));
        this.setUser(user);
        return user;
      })  
    )
    return _model;
  }

  setUser(user:User)
  {
    this.behavior.next(user);
  }
  unsetUser()
  {
    localStorage.removeItem('user');
    this.behavior.next(null);
  }

}
