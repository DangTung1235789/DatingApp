import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/User';
import { take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  //we want to do because we're storing our token as part of our current user inside account service
  //bring account service and inject this into constructor 
  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    /* 
    - we've got current user in our account service when we login, 
    we set that particular property and our current user is unobservable 
    - So what we need to do in order to use the token from account Service, 
    we're going to need to get the current user outside of that observable
    */
    let currentUser!: User;
    /* we need to subcribe to go and get what's inside the observable out of the observable*/
    // we want to take 1 from this observable => we want to complete after we've received 1 of these current users
    //if we didn't do this, our interceptor are going to be initialized when we start our application
    // because they're part of our module and we add them to the previders and they're always going to be around until we close our application
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user );
    //we want to clone this request and add our application header onto it 
    /* attach our token for every request when we login and send that up with our request*/
    if(currentUser){
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`
        }
      })
    }
    return next.handle(request);
  }
}
