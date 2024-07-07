import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Member } from '../models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl=environment.apiUrl;
  constructor(private httpService:HttpClient) { }

  ShowcaseMembers()
  {
    return this.httpService.get<Member[]>(this.baseUrl+'userdata')
  }
  ShowcaseMember(username:string)
  {
    return this.httpService.get<Member>(this.baseUrl+'userdata/'+username)
  }
  UpdateCurrentMember(member:any)
  {
    return this.httpService.put(this.baseUrl+'userdata/',member);
  }
}
