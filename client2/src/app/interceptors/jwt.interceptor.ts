import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { AccountService } from '../services/account.service';
@Injectable()
export class JwtInterceptor
{
  constructor(private accountService:AccountService){}
  intercept(req:HttpRequest<any>,next:HttpHandler):Observable<HttpEvent<any>>
  {
    this.accountService.userstatus$.pipe(take(1)).subscribe({
      next:user=>{
        req=req.clone({
          setHeaders:{
            Authorization:`Bearer ${user?.token}`
          }
        })
      }
    });

    return next.handle(req);
  }
}
