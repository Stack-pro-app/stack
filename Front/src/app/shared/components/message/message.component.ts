import { DatePipe } from '@angular/common';
import { UserService } from './../../../core/services/user.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [],
  templateUrl: './message.component.html',
  styleUrl: './message.component.css',
  providers: [DatePipe],
})
export class MessageComponent implements OnInit {
  constructor
  (private userService: UserService,
    private datePipe:DatePipe
  ) {
  }
  @Input() message: any;
  chat : any ={
    message : "",
    username : ""
  }

ngOnInit(): void {
    this.getUserById();
}

  getUserById() {
    this.userService.getUserById(this.message.userId).subscribe({
      next: (response) => {
        console.log("the users that sent the message ",response);
        this.chat = {
          message:this.message.message,
          username : response.result.name,
        }

      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  OnClick(){
    console.log(this.message)
  }

  timeDifference(previous: string): string {
    const current = new Date().getTime();
    const previousDate = new Date(previous).getTime();
    const elapsed = current - previousDate;

    const seconds = Math.floor(elapsed / 1000);
    const minutes = Math.floor(seconds / 60);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);

    if (days > 0) {
      return days === 1 ? '1 day ago' : `${days} days ago`;
    } else if (hours > 0) {
      return hours === 1 ? '1 hour ago' : `${hours} hours ago`;
    } else if (minutes > 0) {
      return minutes === 1 ? '1 minute ago' : `${minutes} minutes ago`;
    } else {
      return seconds < 5 ? 'just now' : `${seconds} seconds ago`;
    }
  }

  onOpenEdit(data:any){

  }
}
