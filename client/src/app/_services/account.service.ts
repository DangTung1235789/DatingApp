import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { User } from '../_models/User';


//service can be injected into other components or other services in our application
@Injectable({
  providedIn: 'root'
})
/*services are singleton: the data that we store inside the service don't get destroyed 
  until our application is closed down
*/
export class AccountService {
  //we're going to inject the HTTP client into our account service
  baseUrl = 'https://localhost:5001/api/'
  //9. Persisting the login
  //we're going to create unobservable to store our user in 
  private currentUserSource = new ReplaySubject<User>(1);
  public currentUser$ = this.currentUserSource.asObservable();
  //bring in the HttpClient from Angular/common/HTTP 
  constructor(private http: HttpClient) { }
  //what comes after this is account and a login  => we say baseUrl + 'account/login'
  //model: contain our "username and "password" that we send up to the server
  //model duoc khoi tao trong nav.component.ts (model co type la any)
  /*
  we want with the data coming back from the server in the case 
  that we just want to transform the data in some way or select parts of the data
  => using "map"
  */
  //9. Persisting the login
  login(model: any){
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User)=>{
        const user = response;
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }
  //(model: any): we going to receive from our register.component
  //we need to pass up the model that we receive 
  //map thay doi 1 vai thu method, property truoc khi gui ve service
  register(model: any){
    return this.http.post<User>(this.baseUrl + 'account/register',model).pipe(
      map((user: User) =>{
        if(user){
          //pass user
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        //them return user; de check register tren browser
      })
    )
  }
  //9. Persisting the login
  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }
  //9. Persisting the login
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next();
  }
}
