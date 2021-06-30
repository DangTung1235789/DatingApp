import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
  //model: any = {}
  //2. Reactive forms introduction
  registerForm!: FormGroup;
  //do tuoi tren 18: 9. Adding a reusable date input
  maxDate!: Date;
  //11. Client side registration
  validationErrors: string[] = [];
  //15. Hooking up the register method to the service (hooking up: ket noi)
  //we need to bring in the account Service
  // Adding a toast service for notifications
  // 7. Using the form builder service
  constructor(private accountService: AccountService, private toastr: ToastrService,
     private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    //initialize our form
    this.intitializeForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }
  //2. Reactive forms introduction and 3. Client side validation (xác thực) bắt buộc phải nhập vào phần register
  intitializeForm(){
    // 7. Using the form builder service
    // 8. Expanding the register form
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['',[Validators.required,Validators.minLength(4), Validators.maxLength(8)]],
      //4. Adding custom validators (check password vs confirmPassword)
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    })
  }
  //4. Adding custom validators (check password vs confirmPassword)
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      const controls = control?.parent?.controls as { [key: string]: AbstractControl; };
      let matchToControl = null;    
      if(controls) matchToControl = controls[matchTo];       
      return control?.value === matchToControl?.value ? null : { isMatching: true }
    }
  }
  //12. Adding a register form()
  //15. Hooking up the register method to the service (hooking up: ket noi)
  //we inject the account service into this component 
  //in our account service register method, we're subcribing to a response that we get back from register method
  //subcribe de lam gi do, check loi tren browser 
  //11. Client side registration
  register(){
    //console.log(this.registerForm.value);
    this.accountService.register(this.registerForm.value).subscribe(response =>{
      //console.log(response);
      //this.cancel();
      this.router.navigateByUrl('/members');
    }, error =>{
      //console.log(error);
      // Adding a toast service for notifications
      //this.toastr.error(error.error);
      this.validationErrors = error;
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
function password(password: any): ValidatorFn {
  throw new Error('Function not implemented.');
}

