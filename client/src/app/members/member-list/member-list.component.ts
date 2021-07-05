import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/User';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';


@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  //will create a property to store the members
  //we can store inside is an array of members
  //members: Member[] = []
  //9. Using the service to store state
  //members$!: Observable<Member[]>;
  //5. Setting up client pagination
  members!: Member[];
  pagination!: Pagination;
  userParams!: UserParams;
  user!: User;
  //10. Adding filter buttons to the client
  genderList = [{value: 'male', display: 'Males'},{value: 'female', display: 'Females'}];
  //inject the service
  constructor(private memberService: MembersService) {
    //18. Remembering the filters for a user in the service 
    this.userParams = this.memberService.getUserParams();
  }
  //9. Using the service to store state
  // 5. Setting up client pagination
  //khi chạy component này thì nó chạy constructor với ngOnInit
  ngOnInit(): void {
    this.loadMembers();
    
  }
  //5. Retrieving (lấy lại) the list of members
  //subcribe it's an observable that we're returning from this we need to subcribe and pass members (members =>{})
  //bother any error handling from this method because we're taken care of that inside error interceptor 
  // loadMember(){
  //   this.memberService.getMembers().subscribe(members =>{
  //     this.members = members;
  //   })
  // }
  //5. Setting up client pagination
  loadMembers(){
    //18. Remembering the filters for a user in the service 
    this.memberService.setUserParams(this.userParams);
    this.memberService.getMembers(this.userParams).subscribe(response =>{
      this.members = response.result;
      this.pagination = response.pagination;
    })
  }
  //10. Adding filter buttons to the client
  resetFilters(){
    this.userParams = this.memberService.resetUserParams();
    this.loadMembers();
  }
  pageChanged(event: any){
    this.userParams.pageNumber = event.page;
    this.memberService.setUserParams(this.userParams);
    this.loadMembers();
  }

}
