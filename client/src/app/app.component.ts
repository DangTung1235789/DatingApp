
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './_models/User';
import { AccountService } from './_services/account.service';
import { PresenceService } from './_services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating app';
  //khoi tao model user
  //KHAI BAO DU LIEU USERS: ANY(BAT KE LA TYPE NAO )
  users: any;

  constructor(private accountService: AccountService, private presence: PresenceService) {}

  ngOnInit() {
   
    //9. Persisting the login
    this.setCurrentUser();
  }
  //9. Persisting the login
  /*
  we're making the effort to go and get the token from local storage or 
  getting the user obj from local storage and then we're setting that in our account service
  const user: User = JSON.parse(localStorage.getItem('user'));
  const user: User = JSON.parse(localStorage.getItem('user') || '{}');
  */
  Null: any = null;
  setCurrentUser(){
    const user: User = JSON.parse(localStorage.getItem('user') || this.Null ); //loi load 
    // 17.4. Client side SignalR
    if(user)
    {
      this.accountService.setCurrentUser(user);  
      this.presence.createHubConnection(user);
    }
      
  }
 
}
