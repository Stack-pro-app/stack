import { CommonModule } from '@angular/common';
import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { MessageComponent } from '../message/message.component';
import { ChannelService } from '../../../core/services/Channel/channel.service';
import { ChatService } from '../../../core/services/Chat/chat.service';
import { Channel } from '../../../core/Models/channel';

@Component({
  selector: 'app-channel',
  standalone: true,
  imports: [CommonModule, MessageComponent],
  templateUrl: './channel.component.html',
  styleUrl: './channel.component.css',
})
export class ChannelComponent implements OnInit, OnChanges {
  @Input({ required: true }) currentChannelP: any;
  messages: any[] = [];
  //add the messaging service here
  constructor(private service: ChatService) {}
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentChannelP'] && changes['currentChannelP'].currentValue) {
      console.log('changes here', this.currentChannelP);
      this.getMessages();
    }
  }
  ngOnInit(): void {
    console.log('heeeeeeeelo');
    this.getMessages();
    console.log('Received channel : ', this.currentChannelP);
  }
  getMessages() {
    this.service.GetMessages(this.currentChannelP.id, 1).subscribe({
      next: (response) => {
        this.messages = response.result;
        console.log(this.messages);
      },
      error: (error) => {
        console.error('Getting Messages  error', error);
      },
      complete: () => {
        console.info('completed');
      },
    });
  }
}
