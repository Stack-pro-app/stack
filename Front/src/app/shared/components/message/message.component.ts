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
  profilePic: string = "../../../../assets/img/user-square.svg";
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
        console.log(response.result);

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
