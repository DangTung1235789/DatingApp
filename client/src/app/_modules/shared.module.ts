import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import {TabsModule} from 'ngx-bootstrap/tabs';
import {NgxGalleryModule} from '@kolkov/ngx-gallery';
import {FileUploadModule} from 'ng2-file-upload';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { TimeagoModule } from 'ngx-timeago';

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
    NgxGalleryModule,
    //9. Adding a photo uploader
    FileUploadModule,
    //9. Adding a reusable date input
    BsDatepickerModule.forRoot(),
    //6. Using the angular bootstrap pagination component
    PaginationModule.forRoot(),
    // angular bootstrap => for root
    //14. Sorting on the client
    ButtonsModule.forRoot(),
    TimeagoModule.forRoot()
    //we want what inside imports to be available everywhere else, need to export the module
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    NgxGalleryModule,
    FileUploadModule, 
    BsDatepickerModule,
    PaginationModule,
    ButtonsModule,
    TimeagoModule  
  ]
})
export class SharedModule { }
