import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuardGuard } from './guards/auth-guard.guard';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { preventPageUnsavedGuard } from './guards/prevent-page-unsaved.guard';

const routes: Routes = [
  {path:'',
  canActivate:[authGuardGuard],
  runGuardsAndResolvers:'always',
  children:[
    {path:'',component:MemberListComponent},
    {path:'members',component:MemberListComponent},
    {path:'members/:username',component:MemberDetailComponent},
    {path:'member/edit',component:MemberEditComponent,canDeactivate:[preventPageUnsavedGuard]},
    {path:'lists',component:ListsComponent},
    {path:'messages',component:MessagesComponent},
  ]
  },
  {path:'testerrors',component:TestErrorComponent},
  {path:'register',component:HomeComponent},
  {path:'not-found',component:NotFoundComponent},
  {path:'server-error',component:ServerErrorComponent},
  {path:'**',component:NotFoundComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
