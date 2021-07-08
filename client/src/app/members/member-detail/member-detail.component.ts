import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import {NgxGalleryOptions} from '@kolkov/ngx-gallery';
import {NgxGalleryImage} from '@kolkov/ngx-gallery';
import {NgxGalleryAnimation} from '@kolkov/ngx-gallery'
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';
@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  //12. Activating the message tab
  // check in member-detail.component.html => #memberTabs
  @ViewChild('memberTabs', {static: true}) memberTabs!: TabsetComponent;
  member!: Member;
  //13. Adding a photo gallery
  
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];
  activeTab!: TabDirective;
  messages: Message[] = [];
   
  //we want to access when a user clicks on any of these routes
  constructor(private memberService: MembersService, private route: ActivatedRoute, 
    private messageService: MessageService ) { }

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
      this.loadMessages();
    }
  }
}
