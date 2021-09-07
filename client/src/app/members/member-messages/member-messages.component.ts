import { ChangeDetectionStrategy, Input, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  //viewChild la de reset the form va clear content, theo doi noi dung trong member-messages.component.html
  @ViewChild('messageForm') messageForm!: NgForm;
  @Input() username!: string
  @Input() messages: Message[] = [];
  messageContent!: string;

  constructor(public messageService: MessageService) { }

  ngOnInit(): void {
    
  }

  //15. Sending messages
  sendMessage() {
    this.messageService.sendMessage(this.username, this.messageContent).then(() => {

      this.messageForm.reset();
    })
  }

  
}
