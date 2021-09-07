import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
  //todo!: Todo;
  //improt { AccountService } from '../_services/account.service';
  //we're going to do is inject our service inside this component
  // Adding a toast service for notifications
  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }
  
  ngOnInit(): void {
    // this.todo2;
    // console.log(this.todo1);
    // console.log(this.todo2);
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
  /*
  when we want to do is when we have successfully logged in, so we're in the next part of the subcribe,
  we going to take this opportunity to navigate to users to member's component
  */
  login(){
    this.accountService.login(this.model).subscribe(response =>{
      this.router.navigateByUrl('/members');
    }, error =>{
      console.log(error);
      // Adding a toast service for notifications
      this.toastr.error(error.error);
    })
  }
  logout(){
    //9. Persisting the login
    this.accountService.logout();
    //send back to home page
    this.router.navigateByUrl('/');
  }

  // updateTodo(todo: Todo, fieldsToUpdate: Partial<Todo>) {
  //   return { ...todo, ...fieldsToUpdate };
  // }

  // todo1 = {
  //   title: "organize desk",
  //   description: "clear clutter",
  // };
   
  // todo2 = this.updateTodo(this.todo1, {
  //   description: "throw out trash",
  // });
  
}
