import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

/*
when we do encounter(chạm chán) any of these errors, we handle them at a global level inside angular, 
the way that we are going to achieve this is to use an HTTP interceptor (cản trở)
*/
// muốn dùng interceptor thì phải có Exception Middleware
// Khi có lỗi ở HTTP request or response thì mình sẽ bắt chúng nó ở đây và xử lý display them theo mình muốn
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  //we'll add a couple of things in our constructor to inject 
  //add router: reason for this is that for certain types of errors, want to redirect the user to an error page
  //add toastr: because for certain types of error, we might want to display a total notification (app.module.ts, shared.module.ts)
  constructor(private router: Router, private toastr: ToastrService) {}
  /*
  we can either intercept the "request" that goes out or
  the response that the comeback in the "next" where we handle the response 
  */
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    //we want to catch any errors from this 
    //di chuyen chuot vao handle ta thay handle(req: HttpRequest<any>): Observable<HttpEvent<any>>
    /*
    the request, we get back from this is observable so in order to do smt with this 
    => use "pipe" to do whatever functionaiity we want inside here 
    */
    return next.handle(request).pipe(
      //error: this is HTTP response
      catchError(error =>{
        if(error){
          switch (error.status) {
            // su dung click error de check (2. Creating an error controller for testing errors)
            //inspect => HttpError (1) => Error(2) => errors (3)
            //validation error
            case 400:
              if(error.error.errors){
                const modalStateErrors = [];
                for(const key in error.error.errors){
                  if(error.error.errors[key]){
                    modalStateErrors.push(error.error.errors[key])
                  }
                }
                //throw these model Stat Error back to the component
                //the reason: if we take our register form 
                throw modalStateErrors.flat(); 
              }
              else if(typeof(error.error) === 'object'){
                this.toastr.error(error.statusText, error.status);
              } else{
                this.toastr.error(error.error, error.status);
              }
              break;
            case 401:
              this.toastr.error(error.statusText, error.status); 
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              //direct them to a server error page
              //we specify that it's going to be the state 
              //{error: error.error}: is going to be the exception that we get back from our API 
              const navigationExtras: NavigationExtras = {state: {error: error.error}};
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toastr.error('Something unexpected went wrong');
              console.log(error);
              break;
          }
        }
        //this is should remove error from catchError
        return throwError(error);
      })
      //we need to  provide all of this in our app.module
      //because Angular comes of its interceptors and adding this to an interceptors array inside Angular
    )
  }
}
