import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { PresenceService } from 'src/app/_services/presence.service';


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

  //6. Setting up the likes functions in the Angular app
  constructor(private memberService: MembersService, private toastr: ToastrService, 
    public presence: PresenceService) { }

  ngOnInit(): void {
  }

  addLike(member: Member){
    this.memberService.addLike(member.username).subscribe(() => {
      this.toastr.success('You have liked ' + member.knownAs);
    })
  }

}
