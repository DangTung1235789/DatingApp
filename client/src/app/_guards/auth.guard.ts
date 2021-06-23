import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree ,Router} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
//6. Adding an Angular route guard (ko cho truy cap lung tung dung ko co authorized)
/*
when we use a Guard, it automatically subcribes to any observables (account.service.ts)
*/
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private toastr: ToastrService) {}

  canActivate(): Observable<boolean> {
    /*
    our current user observable returns a user or it would do if we're subcribing to it 
    we're inside a AuthGuard, it's going to handle the subcription for itself
    so we need to the pipe, map
    */
    return this.accountService.currentUser$.pipe(
      map(user =>{
        if (user) {
          return true;
        }
        else{
          this.toastr.error('You shall not pass!');
          return false;
        }
        
      })
    )
  }

  
}
