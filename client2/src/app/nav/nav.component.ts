import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';
import {NgForm} from '@angular/forms';
import { Observable, Subscription, of } from 'rxjs';
import { User } from '../models/User';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  model:any={username:"Karen",password:"ngit"};
  LoggedIn=false;
  justUser:any={}
  currentUserSubscription:Subscription=new Subscription();

  constructor(private accountservice:AccountService,private routerService:Router,private toastrService:ToastrService) 
  { 
    this.currentUserSubscription=this.accountservice.behavior.subscribe((user)=>
    {
      this.LoggedIn=!!user;
      this.justUser=user;
    });
  }

  ngOnDestroy()
  {
    this.currentUserSubscription.unsubscribe();
  }

  SubmitForm(formvalues:NgForm)
  {

    let _model=this.accountservice.login(this.model);
    _model.subscribe(
    {
      next:(returnedmodel)=>this.routerService.navigateByUrl('/members'),
      error:(error)=>{}
    });

  }
  logout()
  {
    this.accountservice.unsetUser();
    this.routerService.navigateByUrl('/register');
  }
}
