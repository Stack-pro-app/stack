import { CommonModule } from '@angular/common';
import { UserService } from './../../../core/services/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { AudioPlayerComponent } from '../../../features/pages/audio-player/audio-player.component';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [CommonModule,AudioPlayerComponent],
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
  profilePic: string|null = null;
  loading:boolean = false;

ngOnInit(): void {
  this.username = this.message.user?.name ?? "";
  this.getUserById();
}

isUsername():boolean{
  return this.message.user != null && this.message.user != undefined;
}

  getUserById() {
    this.userService.getUserById(this.message.userId?? this.message.UserId).subscribe({
      next: (response) => {
        this.username = response.result.name;
        this.profilePic = response.result.profilePicture?? "../../../../assets/img/user-square.svg";

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
  isAudioFileName(fileName: string | null): boolean {
    if (!fileName) {
      return false;
    }
    const audioExtensions = ['.mp3', '.mpeg', '.wav', '.ogg','stack-audio'];
    const lowerCaseFileName = fileName.toLowerCase();
    return audioExtensions.some(ext => lowerCaseFileName.includes(ext));
  }

  getFileExtension(fileName: string): string {
    return fileName.split('.').pop()!.toLowerCase();
  }

  getFileImg(fileName: string): string {
    const extention = this.getFileExtension(fileName);
    const availableExt = ['pdf','docx','xlsx','pptx','txt','csv'];
    if(availableExt.includes(extention)){
      return "../../../../assets/img/"+extention+".svg";
    }
    return "../../../../assets/img/unknown.svg";
  }

downloadFile(url: string, fileName: string) {
  fetch(url)
    .then(response => response.blob())
    .then(blob => {
      const objectUrl = window.URL.createObjectURL(blob);
      const anchor = document.createElement('a');
      anchor.href = objectUrl;
      anchor.download = fileName;
      document.body.appendChild(anchor);
      anchor.click();
      document.body.removeChild(anchor);
      window.URL.revokeObjectURL(objectUrl);
    });
}

  OnClick(){
    console.log(this.message)
  }


  timeDifference(previous: string): string {
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

  onOpenEdit(data:any){

  }
}
