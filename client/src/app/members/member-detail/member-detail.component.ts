import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import {NgxGalleryOptions} from '@kolkov/ngx-gallery';
import {NgxGalleryImage} from '@kolkov/ngx-gallery';
import {NgxGalleryAnimation} from '@kolkov/ngx-gallery'
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';
import { PresenceService } from 'src/app/_services/presence.service';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/_models/User';
import { take } from 'rxjs/operators';
@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit, OnDestroy {
  //12. Activating the message tab
  // check in member-detail.component.html => #memberTabs
  // viewchild là ánh xạ dữ liệu từ html về ts=> có thể thay đổi dữ liệu hoặc bất kì điều gì
  @ViewChild('memberTabs', {static: true}) memberTabs!: TabsetComponent;
  member!: Member;
  //13. Adding a photo gallery
  
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];
  activeTab!: TabDirective;
  messages: Message[] = [];
  user!: User;
   
  //we want to access when a user clicks on any of these routes
  constructor(public presence: PresenceService, private route: ActivatedRoute, 
    private messageService: MessageService, private accountService: AccountService,
    private router: Router) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
      // 17.15. Notifying users when they receive a message
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    }
  

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.member = data.member;
    })

    // 13. Using query params 
    this.route.queryParams.subscribe(params =>{
      params.tab ? this.selectTab(params.tab) : this.selectTab(0);
    })
    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
    this.galleryImages = this.getImages();
  }
  // loadMember(){
  //   this.memberService.getMember(this.route.snapshot.paramMap.get('username')|| "").subscribe(member => {
  //     this.member = member;
      
  //   })
  // }
  //13. Adding a photo gallery
  getImages() : NgxGalleryImage[]{
    const imageUrls = [];
    for(const photo of this.member.photos){
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    return imageUrls;
  }

  loadMessages(){
    this.messageService.getMessageThread(this.member.username).subscribe(messages =>{
      this.messages = messages;
    })
  }
  //13. Using query params
  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }
  //12. Activating the message tab
  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if(this.activeTab.heading === 'Messages' && this.messages.length === 0){
      this.messageService.createHubConnection(this.user, this.member.username);
    } else {
      this.messageService.stopHubConnection();
    }
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }
}
