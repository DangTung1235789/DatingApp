import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //user from home component
  //this is show u how we can pass from a parent component down to a child component 
  //if we're receiving property from a parent, it's going to be an "input" property
  
  @Output() cancelRegister = new EventEmitter()
  //12. Adding a register form
  model: any = {}
  //15. Hooking up the register method to the service (hooking up: ket noi)
  //we need to bring in the account Service
  // Adding a toast service for notifications
  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  //12. Adding a register form()
  //15. Hooking up the register method to the service (hooking up: ket noi)
  //we inject the account service into this component 
  //in our account service register method, we're subcribing to a response that we get back from register method
  //subcribe de lam gi do, check loi tren browser 
  register(){
    this.accountService.register(this.model).subscribe(response =>{
      console.log(response);
      this.cancel();
    }, error =>{
      console.log(error);
      // Adding a toast service for notifications
      this.toastr.error(error.error);
    })
  }
  //12. Adding a register form
  //when we click "cancel()" on the cancel button, we want to emit a value using EventEmitter()
  //we can pass whatever you want out of this method
  //we want turn off in home.components.html
  cancel(){
    this.cancelRegister.emit(false);
  }
}
