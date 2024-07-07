import { Component } from '@angular/core';
import { Member } from '../../models/member';
import { MembersService } from '../../services/members.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryModule, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css',
  standalone:true,
  imports:[CommonModule,TabsModule,GalleryModule]
})
export class MemberDetailComponent {
  member:Member | undefined;
  images:ImageItem[] = [];

  constructor(private memberService:MembersService,private router:ActivatedRoute){}

  ngOnInit()
  {
    this.loadmember()
  }

  loadmember()
  {
    const username=this.router.snapshot.paramMap.get('username');

    if(!username)
    return;

    this.memberService.ShowcaseMember(username).subscribe({
      next:(member)=>
      {
        this.member=member;
        for(var i in member.photos)
        {
          this.images.push(new ImageItem({src:member.photos[i].url,thumb:member.photos[i].url}));
          this.images.push(new ImageItem({src:member.photos[i].url,thumb:member.photos[i].url}));
        }
      }
    });
  }
}
