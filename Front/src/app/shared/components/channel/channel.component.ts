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
import { SignalrService } from '../../../core/services/signalr/signalr.service';

@Component({
  selector: 'app-channel',
  standalone: true,
  imports: [CommonModule, MessageComponent],
  templateUrl: './channel.component.html',
  styleUrl: './channel.component.css',
})
export class ChannelComponent implements OnInit, OnChanges {
  @Input({ required: true }) currentChannelP!: Channel;
  messages: any[] = [];
  //add the messaging service here
  constructor(private service: ChatService,
    private channelService: ChannelService,
    private signalrService : SignalrService) {

    }



  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentChannelP'] && changes['currentChannelP'].currentValue) {
     //console.log("Changed WorkSpace Chanel" ,changes['currentChannelP']);
          console.log(' ChanelStr', this.currentChannelP.channelString);
          this.getMessages();

      this.signalrService.joinChannel(this.currentChannelP.channelString);

    }
  }
  ngOnInit(): void {
    this.signalrService.startConnection().subscribe(() => {
            this.signalrService.joinChannel(this.currentChannelP.channelString);

       this.signalrService.receiveMessage().subscribe((message) => {
        // Before you add the message and show it make sure that you have added the user by using a user getter api
        console.log(message);
        this.messages.push(message);
       });
     });
   this.getMessages();
  }
  getMessages() {
    this.service.GetMessages(this.currentChannelP.id, 1).subscribe({
      next: (response) => {
        this.messages=[];
        this.messages = response.result;
        console.log("The messages",this.messages);
      },
      error: (error) => {
        console.error('Getting Messages  error', error);
      },
      complete: () => {
      },
    });
  }


  onGetChannel(){
    this.channelService.GetChannelById(this.currentChannelP.id).subscribe({
      next: (response) => {
        console.log("The Channel",response);
      },
      error: (error) => {
        console.error('Getting Channel  error', error);
      },
      complete: () => {
        console.log();

      },
    });
  }
}
