import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/User';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  //khoi tao model login
  model: any = {}
  
  //improt { AccountService } from '../_services/account.service';
  //we're going to do is inject our service inside this component
  constructor(public accountService: AccountService) { }
  
  ngOnInit(): void {
  
  }
  //we're going to use our account service to actually login our user
  /*
  login method  is returning to us unobservable, this may issue, 
  unobservable is lazy, it doesn't do anything until we subcribe to the observable 
  then we're going to get response back from our server (response =>)
  the response from our server when we login, we're going to get that user DTO returns to us 
  we'll do just to see what happens => console.log(response)
  */
  //observables 
  login(){
    this.accountService.login(this.model).subscribe(response =>{
      console.log(response);
    }, error =>{
      console.log(error);
    })
  }
  logout(){
    //9. Persisting the login
    this.accountService.logout();
  }
}
