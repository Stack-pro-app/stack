import { CommonModule } from '@angular/common';
import { UserService } from './../../../core/services/user.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './message.component.html',
  styleUrl: './message.component.css',
})
export class MessageComponent implements OnInit {
  constructor
  (private userService: UserService,
  ) {
  }
  @Input() message: any;
  username: string = "";

ngOnInit(): void {
  this.username = this.message.user?.name ?? "";
  console.log(this.message.Attachement_Url);
  console.log(this.message);
  if(!this.isUsername()){
   this.getUserById();
  }
}

isUsername():boolean{
  return this.message.user != null && this.message.user != undefined;
}

  getUserById() {
    this.userService.getUserById(this.message.userId?? this.message.UserId).subscribe({
      next: (response) => {
        this.username = response.result.name;
        console.log(this.username);

      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  isImageFileName(fileName: string | null): boolean {
    if (!fileName) {
      return false;
    }
    const imageExtensions = ['.jpg', '.jpeg', '.png', '.gif', '.bmp'];
    const lowerCaseFileName = fileName.toLowerCase();
    return imageExtensions.some(ext => lowerCaseFileName.endsWith(ext));
  }

  OnClick(){
    console.log(this.message)
  }

  timeDifference(previous: string): string {
    let date = new Date(previous);
    let now = new Date();
    let diff = now.getTime() - date.getTime();
    let hours = Math.floor(diff / 1000 / 60 / 60);
    if(hours == 0){
      let minutes = Math.floor(diff / 1000 / 60);
      if(minutes==0){
        return "Just now";
      }
      return minutes+" minutes ago";
    }
    if(hours<24){
      return hours+" hours ago";
    }
    let days = Math.floor(hours/24);
    return days+" days ago";
  }

  onOpenEdit(data:any){

  }
}
