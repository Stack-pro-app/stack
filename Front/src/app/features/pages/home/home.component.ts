import { Component, OnInit } from '@angular/core';
import { Params, Router, RouterLink } from '@angular/router';
import { StoreService } from '../../../core/services/store/store.service';
import { AuthService } from '../../../core/services/Auth/auth.service';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../header/header.component';
import { ChatUser } from '../../../core/Models/chat-user';
import { UserService } from '../../../core/services/user.service';
import { MainComponent } from '../main/main.component';
import { InvitationsComponent } from '../invitations/invitations.component';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, CommonModule, HeaderComponent,MainComponent,InvitationsComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  loggeduser: ChatUser = {
    authId: '',
    createdAt: '',
    email: '',
    id: 0,
    lastLogin: null,
    name: '',
  };
  par:Params={id:''}

  isLogged: Boolean = false;
  workspaces: any[] = [];

  constructor(private store: StoreService,
     private service: AuthService,
    private userService:UserService,
    private router:Router) {
  }
  decoded = this.store.getUser();
  ngOnInit(): void {
    console.log(this.loggeduser);
    this.isLogged = this.store.isLogged();
    this.service.getChatUser().subscribe({
      next: (response) => {
        this.loggeduser=response.result;
        localStorage.setItem("userId",this.loggeduser.id.toString());
        console.log("User id is",localStorage.getItem('userId'));
         this.userService.getWorkSpaces(this.loggeduser.authId).subscribe({
           next: (response) => {
             console.log(response);
             this.workspaces= response.result.workspaces;
             console.log(response.result);
             this.store.setNotifString(response.result.user.notificationString);
             console.log(this.workspaces);
           },
           error: (error) => {
             console.error('Login error', error);
           },
           complete: () => console.info('complete'),
         });

      },
      error: (error) => {
        console.error('Login error', error);
      },
      complete: () => console.info('complete'),
    });


  }
  OnLogout() {
    this.service.logout();
    this.router.navigate(['/Welcome']);
  }
  onSetAdmin(id:any){
    this.store.setAdmin(id);
    console.log("Admin is set",localStorage.getItem('Admin'));

  }
  }
