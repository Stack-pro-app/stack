import { FileService } from './../../../services/file.service';
import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { MessageComponent } from '../message/message.component';
import { ChannelService } from '../../../core/services/Channel/channel.service';
import { ChatService } from '../../../core/services/Chat/chat.service';
import { Channel } from '../../../core/Models/channel';
import { SignalrService } from '../../../core/services/signalr/signalr.service';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { StoreService } from '../../../core/services/store/store.service';

@Component({
  selector: 'app-channel',
  standalone: true,
  imports: [CommonModule, MessageComponent,InfiniteScrollModule],
  templateUrl: './channel.component.html',
  styleUrl: './channel.component.css',
})
export class ChannelComponent implements OnInit, OnChanges {
  @ViewChild('scrollable') scrollable: ElementRef | undefined;
  @Input({ required: true }) currentChannelP!: Channel;
  page: number = 1;
  throttle = 0;
  distance = 1;
  users: any[] = [];
  messages: any[] = [];
  //add the messaging service here
  constructor(private service: ChatService,
    private channelService: ChannelService,
    private fileService: FileService,
    private storeService: StoreService,
    private signalrService : SignalrService) {

    }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentChannelP'] && changes['currentChannelP'].currentValue) {
     //console.log("Changed WorkSpace Chanel" ,changes['currentChannelP']);
     this.signalrService.joinChannel(this.currentChannelP.channelString);
          this.page = 1;
          this.getMessages(this.page);


    }
  }
  ngOnInit(): void {
    this.page=1;
    this.signalrService.startConnection().subscribe(() => {
      this.signalrService.joinChannel(this.currentChannelP.channelString);

       this.signalrService.receiveMessage().subscribe((message) => {
        this.AddMessage(message);
       });
     });
   this.getMessages(this.page);
  }
  AddMessage(message: any) {
    console.log(message.userId??message.UserId);
    console.log(localStorage.getItem('userId'));
    console.log(message.userId??message.UserId == localStorage.getItem('userId'));
    if((message.userId??message.UserId) == localStorage.getItem('userId')){
    this.storeService.skeletonMessage = false;
    if(message.attachement_Url??message.Attachement_Url){
      this.fileService.fileSent = false;
    }
    }
    message.created_at = this.formatDate(new Date().toISOString());
    this.messages.push(message);
  }
  formatDate(date: string): Date {
    return new Date(date);
  }
  Fetch(){
    this.page++;
    this.getOldMessages(this.page);
  }
  getOldMessages(page:Number){
    console.log("Getting old messages "+this.page);
    this.service.GetMessages(this.currentChannelP.id, page).subscribe({
      next: (response) => {
        if(response.result.length > 0){
          this.messages = [...this.messages, ...response.result];
        }
      },
      error: (error) => {
        console.error('Getting Messages  error', error);
      },
      complete: () => {
        this.sortItemsByDate();
      },
    });
  }
  getMessages(page:Number) {
    this.service.GetMessages(this.currentChannelP.id, page).subscribe({
      next: (response) => {
        this.messages=[];
        this.messages = response.result;
      },
      error: (error) => {
        console.error('Getting Messages  error', error);
      },
      complete: () => {
        this.sortItemsByDate();
        this.scrollToBottom();
      },
    });
  }

  sortItemsByDate() {
    this.messages.sort((a, b) => {
      const dateA = this.formatDate(a.created_at);
      const dateB = this.formatDate(b.created_at);
      return dateA.getTime() - dateB.getTime();
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

  IsSkeletonMessage(): boolean {
    return this.storeService.skeletonMessage;
  }

  scrollToBottom(): void {
    try {
      setTimeout(() => {
        if (this.scrollable && this.scrollable.nativeElement) {
          this.scrollable.nativeElement.scrollTop = this.scrollable.nativeElement.scrollHeight;
        }
      }, 100);
    } catch (err) {}
  }
}
