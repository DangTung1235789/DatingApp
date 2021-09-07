import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { ConfirmService } from '../_services/confirm.service';

@Injectable({
  providedIn: 'root'
})
//5. Adding a Can Deactivate (hủy kích hoạt) route guard
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  /*
      - this is going to give us access to our edit form because we're going to need to check the status 
      of the form inside here
      - return boolean 
  */
  constructor(private confirmService: ConfirmService){}
  canDeactivate(component: MemberEditComponent):  Observable<boolean> | boolean {
      //check dirty 
      if(component.editForm.dirty){
        //this is give them an option to say "yes" or "no"
        return this.confirmService.confirm();
      }   
      //return yes, => we can deactive this component
      return true;
  }
  
}
