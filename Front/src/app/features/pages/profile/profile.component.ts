import { AuthService } from './../../../core/services/Auth/auth.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { StoreService } from '../../../core/services/store/store.service';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../core/services/user.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent implements OnInit {
  @ViewChild("fileUpload", {static: false}) fileUpload: ElementRef | undefined;
  themes:any[]= [
      "light",
      "dark",
      "cupcake",
      "bumblebee",
      "emerald",
      "corporate",
      "synthwave",
      "retro",
      "cyberpunk",
      "valentine",
      "halloween",
      "garden",
      "forest",
      "aqua",
      "lofi",
      "pastel",
      "fantasy",
      "wireframe",
      "black",
      "luxury",
      "dracula",
      "cmyk",
      "autumn",
      "business",
      "acid",
      "lemonade",
      "night",
      "coffee",
      "winter",
      "dim",
      "nord",
      "sunset",
    ];
    user : any = {
    };
  // user: any = {
  //   name: 'Hayaoui Mouad',
  //   location: 'CasaBlanca',
  //   role: 'Admin',
  //   University: 'Abdelmalk Essaidi',
  //   Description:
  //     'A computer science student is an individual immersed in the study of computer systems, algorithms, programming languages, and the theoretical foundations of computing. Passionate about solving complex problems, this student learns to design and implement software, analyze data structures, and understand the intricacies of hardware and software interactions. They often engage in coding projects, collaborate with peers on innovative solutions, and stay abreast of the latest advancements in technology. With a curiosity for innovation and a love for logical reasoning, a computer science student is on a journey to acquire the skills and knowledge needed to navigate the ever-evolving landscape of computing.',
  //   Status: 'Online',
  // };
  constructor(private store: StoreService,private authService:AuthService,private userService:UserService) {}
  ngOnInit(): void {
    this.authService.getChatUser().subscribe({
      next: (response) => {
        console.log(response);
        this.user = response.result;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
  OnImageUpload(event: any) {
    const file = event.target.files[0];
    console.log(file);
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
    console.log(this.fileUpload?.nativeElement.files[0])
    console.log(decoded);
    profilePic.append('authId',decoded.sub);
    console.log("sent");
    this.userService.updateProfilePic(profilePic).subscribe({
      next: (response) => {
        console.log("reeived");
        console.log(response);
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
