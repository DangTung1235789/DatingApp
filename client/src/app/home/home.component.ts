import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  //11. Adding a home page, check condition
  registerMode = false;
  
  constructor() { }

  ngOnInit(): void {
   
  }
  //11. Adding a home page, reverse boolean
  registerToggle(){
    this.registerMode = !this.registerMode
  }
  
  //(event: boolean): that what we're emitting from our child component
  //because we set the value of false (in register.component.ts) when we emit this event 
  //then "event" should be false => will stop display the register Form 
  cancelRegisterMode(event: boolean){
    this.registerMode = event; 
  }
}
