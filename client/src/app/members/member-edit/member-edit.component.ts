import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
/*
- member-edit.component.ts là trang xử lí lấy dữ liệu từ Account Service, 
MemberService rồi truyền dữ liệu vào member-edit.component.html 
- Account Service, MemberService lấy dữ liệu từ API
- member-edit.component.html lấy dữ liệu từ .ts rồi hiển thị lên browser 
- trong member-edit.component.html thì dùng rootLink để chỉ dẫn các đường link (app-routing.module.ts)
- trong member-edit.component.html: truyền dữ liều từ parent xuống children và ngược lại 
=> tức là từ component này sang component khác
*/
export class MemberEditComponent implements OnInit {
  //this is gives us access to this template form inside our components 
  @ViewChild('editForm') /*give a name inside component */ editForm!: NgForm;
  member!: Member;
  user!: User;
  //5. Adding a Can Deactivate (hủy kích hoạt) route guard
  //khi user thoát ra ngoài, tắt browser thì sẽ hiện thông báo chưa save khi đang chỉnh sửa thông tin
  // if we want to do smt before the browser is close, then we've got an option to do this: @HostListener
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }
  /*
  - we is get hold of the current user from our account service and 
  we'll use that to get the username for that user so we can go and fetch that particular member
  */
 /*
    - the current user is unobservable and what we need to do is get the user out of that observable 
    and use that for our obj (user) 
    - we need to access this obj (user) and we can't work with unobservable to do that
    so we need to get it out of there 
    - and now, our users will have the current user from our account service 
 */
  constructor(private accountService: AccountService, private memberService: MembersService, 
    private toastr: ToastrService) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    this.memberService.getMember(this.user.username).subscribe(member =>{
      this.member = member;
    })
  }
  //this is going to be the update member after we've submitted our form 
  //7. Updating the user in the client app
  updateMember(){
    this.memberService.updateMember(this.member).subscribe(()=> {
      this.toastr.success('Profile updated successfully');
      this.editForm.reset(this.member);
    })
    
  }
}
