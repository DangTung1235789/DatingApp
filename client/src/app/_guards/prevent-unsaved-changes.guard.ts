import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  /*
      - this is going to give us access to our edit form because we're going to need to check the status 
      of the form inside here
      - return boolean 
  */
  canDeactivate(component: MemberEditComponent):  boolean  {
      //check dirty 
      if(component.editForm.dirty){
        //this is give them an option to say "yes" or "no"
        return confirm('Are you sure you want to continue? Any saved changes will be lost');
      }   
      //return yes, => we can deactive this component
      return true;
  }
  
}
