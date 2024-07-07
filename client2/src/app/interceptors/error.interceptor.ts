import { HttpEvent, HttpHandler, HttpInterceptorFn, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, catchError, of } from 'rxjs';

@Injectable()
export class ErrorInterceptor
{
  constructor(private toastrService:ToastrService,private router:Router){}
  intercept(req:HttpRequest<any>,next:HttpHandler):Observable<HttpEvent<any>>
  {
    return next.handle(req).pipe(
      catchError((error)=>{

        if(error.status==500)
        {
          console.log(error);
          const navigationextras:NavigationExtras={state:{error:error.error}};
          this.router.navigateByUrl('/server-error',navigationextras);
        }
        else if(error.status==401)
        this.toastrService.error(`401:${error.error}`);
        else if(error.status==400)
        {
          if(error.error.errors)
          {
            let senderrors=[]
            for(let i in error.error.errors )
            senderrors.push(i);
            this.toastrService.error("400 but more!");
            throw senderrors;
          }
          else
          this.toastrService.error(`400:,${error.error}`);
        }
        else if(error.status==404)
        this.router.navigateByUrl('/not-found');
        else
        this.toastrService.error("Something went wrong!");
        throw error;
      })
    );
  }
}
// export const errorInterceptor:HttpInterceptorFn = (req,next) => {
//   return next(req).pipe(
//     catchError((error)=>{
//       if(error.status==500)
//       console.log("500:",error.error);
//       if(error.status==401)
//       console.log("401:",error.error);
//       if(error.status==400)
//       console.log("400:",error.error);
//       if(error.status==404)
//       console.log("404:",error.error);
//       throw new error("Random stuff");
//     })
//   );
// };
