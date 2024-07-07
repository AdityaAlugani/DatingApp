import { Component, HostListener, ViewChild } from '@angular/core';
import { Member } from '../../models/member';
import { User } from '../../models/User';
import { AccountService } from '../../services/account.service';
import { MembersService } from '../../services/members.service';
import { take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent {

  member:Member | undefined;
  user:User | null=null;
  @ViewChild('editForm') EditForm:NgForm | undefined;
  @HostListener('window:beforeunload',['$event']) 
  unloadnotification($event:any)
  {
    if(this.EditForm?.dirty)
    $event.returnValue=true; 
  }


  constructor(accountService:AccountService,private memberService:MembersService,private toastr:ToastrService)
  {
    accountService.userstatus$.pipe(take(1)).subscribe({
      next:(user)=>{
        this.user=user;
      }
    })
  }

  ngOnInit()
  {
    this.memberpopulate();
  }
  memberpopulate()
  {
    if(this.user==null)
    return;

    this.memberService.ShowcaseMember(this.user.userName).pipe(take(1)).subscribe({
      next:(member)=>this.member=member
    }); 
  }

  UpdateMember() {
    this.memberService.UpdateCurrentMember(this.member).subscribe({
      next:()=>{
        this.toastr.success("Member Updated Successfully");
        this.EditForm?.reset(this.member);
      }
    });
  }
}
