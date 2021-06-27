import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';


@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  /*
    - input property because we're going to be receiving this data from its parent, which is the member 
    list component
  */
  // member! : khoi tao nhung ko assign
  @Input() member!: Member;
  constructor() { }

  ngOnInit(): void {
  }

}
