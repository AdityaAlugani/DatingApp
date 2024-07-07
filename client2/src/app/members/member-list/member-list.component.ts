import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MembersService } from '../../services/members.service';
import { Member } from '../../models/member';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent {
  members:Member[]=[];

  constructor(private memberService:MembersService){}

  ngOnInit()
  {
    this.showAllMembers();
  }

  showAllMembers()
  {
    this.memberService.ShowcaseMembers().subscribe({
      next:(members)=>this.members=members,
      error:(error)=>console.log(error),
      complete:()=>console.log("Completed!")
    });
  }
}
