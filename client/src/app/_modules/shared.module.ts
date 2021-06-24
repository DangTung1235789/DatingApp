import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';


//9. Tidying up the app module by using a shared module
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    //dropdown in wellcome user in front end
    BsDropdownModule.forRoot(),
    // Adding a toast service for notifications
    //notification 
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
    //we want what inside imports to be available everywhere else, need to export the module
  ],
  exports: [
    BsDropdownModule,
    ToastrModule
  ]
})
export class SharedModule { }
