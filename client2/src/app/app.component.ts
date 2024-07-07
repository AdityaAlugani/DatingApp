import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';
import { User } from './models/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'client';
  // users:any;

  constructor(private http:HttpClient,private accountservice:AccountService){}

  ngOnInit()
  {
    // this.getUsersData();
    this.LocalStorageAssign();
  }
  LocalStorageAssign() {
      let userdetails:string | null=localStorage.getItem('user');
      if(userdetails==null)
      return;
      // console.log(userdetails);
      let _user:User=JSON.parse(userdetails);
      this.accountservice.behavior.next(_user);
  }
  // getUsersData() {
  //   this.http.get('http://localhost:5000/api/userdata').subscribe({
  //     next:response=>this.users=response,
  //     error:err=>console.log(err.message),
  //     complete:()=>console.log("Completed Request",this.users)
  //   })
  // }

}
