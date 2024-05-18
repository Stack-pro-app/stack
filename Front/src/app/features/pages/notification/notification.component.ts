import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { NotificationService } from '../../../core/services/Notification/notification.service';
import { SignalrNotifService } from '../../../core/services/signalr-notif/signalr-notif.service';
import { Notification, ResponseDto } from '../../../core/Models/notification';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [InfiniteScrollModule,ReactiveFormsModule,CommonModule],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css'
})
export class NotificationComponent implements OnInit,OnChanges{

  constructor(private notificationService: NotificationService,
    private signalRNotif: SignalrNotifService) {

  }
  ngOnChanges(changes: SimpleChanges): void {

  }
  ngOnInit(): void {
    this.signalRNotif.startConnection().subscribe(() => {
      console.log('SignalR connection established');
      console.log(this.notifString);
      this.signalRNotif.joinGroup(this.notifString);
    }, error => {
      console.error('Error starting SignalR connection:', error);
    });

    // Receive new notifications
    this.signalRNotif.receiveMessage().subscribe((message: any) => {
      console.log(message);
      this.NewNotif = true;
    }, error => {
      console.error('Error receiving message:', error);
    })
  }
  //============================================================================
  throttle = 0;
  distance = 1;
  ShowOld: boolean = false;
  BellClick: boolean = false;
  notifications: Notification[]=[];
  NewNotif: boolean = false;
  Loading: boolean = true;
  page: number = 1;
  notifString: string = localStorage.getItem('notifString')?? "90102568-5B09-445D-BBA8-125F2741BA9B";

  Fetch(){
    if(this.ShowOld){
      this.FetchOld();
    }
    else{
      this.FetchNew();
    }
  }

  ToggleOldAndFetch(){
    this.ShowOld = true;
    this.notifications = [];
    this.page = 1;
    this.Fetch();
  }
  ToggleNewAndFetch(){
    this.ShowOld = false;
    this.notifications = [];
    this.page=1;
    this.Fetch();
  }

  FetchNew(){
    this.ShowOld = false;
    if(!this.isNotifAvailable())
      this.ResetPages();
    if(this.page == 1){
    this.Loading = true;
    this.notifications = [];
    this.notificationService.GetUnSeenNotifications(this.notifString+`?page=1`).subscribe({
      next: (response:ResponseDto) => {
      this.notifications = response.result;
      this.Loading = false;
    },
    error: (error) => {
      console.error(error);
    },
    complete: () => {},
    });
    }
    else{
      this.notificationService.GetUnSeenNotifications(this.notifString+`?page=${this.page}`).subscribe({
        next: (response:ResponseDto) => {
        this.notifications = this.notifications.concat(response.result);
      },
      error: (error) => {
        console.error(error);
      },
      complete: () => {},
      });
    }
    this.page++;
  };
  FetchOld(){
    this.ShowOld = true;
    if(!this.isNotifAvailable())
      this.ResetPages();
    if(this.page == 1){
    this.Loading = true;
    this.notifications = [];
    this.notificationService.GetSeenNotifications(this.notifString+`?page=1`).subscribe({
      next: (response:ResponseDto) => {
      this.notifications = response.result;
      this.Loading = false;
    },
    error: (error) => {
      console.error(error);
    },
    complete: () => {},
    });
    }
    else{
      this.notificationService.GetSeenNotifications(this.notifString+`?page=${this.page}`).subscribe({
        next: (response:ResponseDto) => {
        this.notifications = this.notifications.concat(response.result);
      },
      error: (error) => {
        console.error(error);
      },
      complete: () => {},
      });
    }
    this.page++;
  };
  isNotifAvailable():boolean{
    return this.notifications.length > 0;
  }
  CheckNotif(){
    this.NewNotif = false;
    this.FetchFirst();
  }
  FetchFirst(){
    this.ShowOld = false;
    this.notifications = [];
    if(!this.BellClick){
      this.BellClick = true;
      this.Fetch();
    }
  }
  ToggleBell(){
    this.BellClick = false;
  }
  toHoursPassed(previous:string){
    const date = new Date(previous);
    date.setHours(date.getHours() + 1);
    const now = new Date();
    const diff = now.getTime() - date.getTime();
    const hours = Math.floor(diff / 1000 / 60 / 60);
    if (hours <= 0) {
      const minutes = Math.floor(diff / 1000 / 60);
      if (minutes <= 0) {
        return "Just now";
      }
      return minutes + (minutes === 1 ? " minute" : " minutes") + " ago";
    }
    if (hours < 24) {
      return hours + (hours === 1 ? " hour" : " hours") + " ago";
    }
    const days = Math.floor(hours / 24);
    return days + (days === 1 ? " day" : " days") + " ago";
  }
  ResetPages():void{
    this.page = 1;
  }
}
