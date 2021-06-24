import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
  /*when we implement our global error handling solution in Angular, we going to be able to easily
  test these different errors and make sure we're generating the correct response that we want to see
  for each of these different types of errors
  */
  baseUrl = 'https://localhost:5001/api/';
  //7. Validation errors
  validationErrors: string[] = [];

  //that we can test those error response from our API
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
  //what we want to do is to see what we get back in the client 
  get404Error(){
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe(response=>{
      console.log(response);
    },error=>{
      console.log(error);
    })
  }
  //what we want to do is to see what we get back in the client 
  get400Error(){
    this.http.get(this.baseUrl + 'buggy/bad-request').subscribe(response=>{
      console.log(response);
    },error=>{
      console.log(error);
    })
  }
  //what we want to do is to see what we get back in the client 
  get500Error(){
    this.http.get(this.baseUrl + 'buggy/server-error').subscribe(response=>{
      console.log(response);
    },error=>{
      console.log(error);
    })
  }
  //what we want to do is to see what we get back in the client 
  get401Error(){
    this.http.get(this.baseUrl + 'buggy/auth').subscribe(response=>{
      console.log(response);
    },error=>{
      console.log(error);
    })
  }
  //what we want to do is to see what we get back in the client 
  get400ValidationError(){
    this.http.post(this.baseUrl + 'account/register',{}).subscribe(response=>{
      console.log(response);
    },error=>{
      console.log(error);
      //7. Validation errors
      this.validationErrors = error;
    })
  }

}
