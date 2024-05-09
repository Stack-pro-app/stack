import { UserService } from './../../../core/services/user.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [],
  templateUrl: './message.component.html',
  styleUrl: './message.component.css',
})
export class MessageComponent implements OnInit {
  constructor
  (private userService: UserService
  ) {
  }
  @Input() message: any;
 
ngOnInit(): void {
    this.getUserById();
}

  getUserById() {
    this.userService.getUserById(this.message.userId).subscribe({
      next: (response) => {
        console.log("the users that sent the message ",response);
        this.message = {
          message:this.message.message,
          username : response.result.name,
        }

      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  onOpenEdit(data:any){

  }
}
