import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
//9. Adding a server error page
export class ServerErrorComponent implements OnInit {
  error: any;
  //we need to inject our router (in error.interceptor.ts) into this
  //we've got  access to the router state (router se truy cap vao Router)
  //truy cap bang constructor, OnInit ko dung de truy cap 
  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    /*
      navigation? : is check the navigation
      - because we don't know if we're going to have any of this information
      - what's going to happen as soon as the user refreshes their page, then we immedeliately lose whatever
      inside this navigation state
      - we only get it once when the route is activated, when we redirect the user to this server error page
    */
    this.error = navigation?.extras?.state?.error;
  }

  ngOnInit(): void {
  }

}
