import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { User } from 'src/app/_models/User';


@Component({
  selector: 'app-roles-model',
  templateUrl: './roles-model.component.html',
  styleUrls: ['./roles-model.component.css']
})
export class RolesModelComponent implements OnInit {
  @Output() updateSelectedRoles = new EventEmitter();
  user!: User;
  roles!: any[];

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }

  updateRoles() {
    this.updateSelectedRoles.emit(this.roles); // neu loi thi check o day
    this.bsModalRef.hide();
  }

}
