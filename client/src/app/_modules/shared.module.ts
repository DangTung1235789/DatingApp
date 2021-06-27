import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import {TabsModule} from 'ngx-bootstrap/tabs';
import {NgxGalleryModule} from '@kolkov/ngx-gallery';

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
    }),
    //12. Styling the member detailed page part two
    TabsModule.forRoot(),
    // Adding a photo gallery
    NgxGalleryModule
    //we want what inside imports to be available everywhere else, need to export the module
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    NgxGalleryModule 
  ]
})
export class SharedModule { }
