import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
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
  //in environment.ts
  baseUrl = environment.apiUrl;
  null: any = null;
  //9. Persisting the login
  //we're going to create unobservable to store our user in 
  /*
    the reason we set this up as an observable that can be observed by other components or other classes in our application
    whenever smt subcribes to this, then it's going to be notified if anything changes 
    this is the reason for using observable in the first place of Guards is able to subcribes to this
    auth.guard.ts
  */
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
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
          this.setCurrentUser(user);
        }
      })
    )
  }
  //(model: any): we going to receive from our register.component
  //we need to pass up the model that we receive 
  //pipe thay doi 1 vai thu method, property truoc khi gui ve service
  //phương thức map() cho phép bạn chuyển đổi các phần tử của mảng 
  register(model: any){
    return this.http.post<User>(this.baseUrl + 'account/register',model).pipe(
      map((user: User) =>{
        if(user){
          //pass user
          this.setCurrentUser(user);
        }
        //them return user; de check register tren browser
      })
    )
  }
  //9. Persisting the login
  setCurrentUser(user: User){
    localStorage.setItem('user',JSON.stringify(user));
    this.currentUserSource.next(user);
  }
  //9. Persisting the login
  
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(this.null); //loi load 
  }
}
