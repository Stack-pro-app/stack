import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../../core/services/user.service';
import { CommonModule } from '@angular/common';
import { StoreService } from '../../../core/services/store/store.service';
import { AuthService } from '../../../core/services/Auth/auth.service';

@Component({
  selector: 'app-profile-2',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile-2.component.html',
  styleUrl: './profile-2.component.css'
})
export class Profile2Component implements OnInit{
  @ViewChild("fileUpload", {static: false}) fileUpload: ElementRef | undefined;
    constructor(private authService:AuthService,private userService:UserService,private store:StoreService) { }
    user: any;
    Loading:boolean=false;

    ngOnInit(): void {
      if (localStorage.getItem('userId')) {
        this.authService.getChatUser().subscribe((data) => {
          this.user = data.result;
        });
      }
    }

    isProfilePicture():boolean{
      if(this.user){
        if(this.user.profilePicture){
          return true;
        }
      }
      return false;
    }

    OnImageUpload(event: any) {
      this.Loading=true;
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        this.user.profilePicture = reader.result;
      };
      reader.readAsDataURL(file);
      this.OnUpload();
    }
    OnUpload():boolean{
      const decoded = this.store.getUser();
      const profilePic = new FormData();
      if(this.fileUpload?.nativeElement.files[0]){
      profilePic.append('picture',this.fileUpload?.nativeElement.files[0]);
      profilePic.append('authId',decoded.sub);
      this.userService.updateProfilePic(profilePic).subscribe({
        next: (response) => {
          this.Loading=false;
        },
        error: (error) => {
          console.log(error);
          return false;
        },
        complete: () => {
          return true;
        }
      });
      }
      return false;
    }

}
