import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';


@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  //will create a property to store the members
  //we can store inside is an array of members
  members: Member[] = [];
  
  //inject the service
  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    this.loadMember();
    
  }
  //5. Retrieving (lấy lại) the list of members
  //subcribe it's an observable that we're returning from this we need to subcribe and pass members (members =>{})
  //bother any error handling from this method because we're taken care of that inside error interceptor 
  loadMember(){
    this.memberService.getMembers().subscribe(members =>{
      this.members = members;
    })
  }
  

}
