import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';
import { map } from 'rxjs';

export const authGuardGuard: CanActivateFn = (route, state) => {
  const toastr=inject(ToastrService);
  const accountservice=inject(AccountService);
  const routing=inject(Router);
  return accountservice.userstatus$.pipe(
    map((status)=>
    {
      if(status)
      return true;
      else
      {
        toastr.error('Please login first');
        routing.navigateByUrl('/register')
        return false;
      }
    })
  );
};
