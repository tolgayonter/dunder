import { Component, Input, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.scss'],
})
export class MemberMessagesComponent {
  @Input() username: string | undefined;
  messageContent = '';
  @ViewChild('messageForm') messageForm: NgForm | undefined;

  constructor(public messageService: MessageService) {}

  sendMessage() {
    if (!this.username) return;
    this.messageService
      .sendMessage(this.username, this.messageContent)
      .then(() => {
        this.messageForm?.reset();
      });
  }
}
