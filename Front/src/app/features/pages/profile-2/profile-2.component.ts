import { Component, Input, OnInit } from '@angular/core';
import { UserService } from '../../../core/services/user.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile-2',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile-2.component.html',
  styleUrl: './profile-2.component.css'
})
export class Profile2Component implements OnInit{
    constructor(private userService:UserService) { }
    user: any;

    ngOnInit(): void {
      if (localStorage.getItem('userId')) {
        const userId: number = Number.parseInt(localStorage.getItem('userId')??'0');
        this.userService.getUserById(userId).subscribe((data) => {
          this.user = data.result;
          console.log(this.user);
        });
      }
    }

    onOpen(): void {
    }

}
