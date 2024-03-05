import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { StoreService } from '../../../core/services/store/store.service';
import { AuthService } from '../../../core/services/Auth/auth.service';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../header/header.component';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink,CommonModule,HeaderComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  loggeduser: any = '';
  isLogged:Boolean=false;
  userEmail: any = 'Hayaouimouad@gmail.com';
  workspaces: any[] = [
    {
      name: 'First WorksSpace',
    },
    {
      name: 'Second WorksSpace',
    },
  ];

  constructor(private store: StoreService, private service: AuthService) {}
  decoded = this.store.getUser();
  ngOnInit(): void {
    this.loggeduser = this.store.isLogged();
    console.log(this.store.getUser());
    this.isLogged=this.store.isLogged();
    //    this.userEmail=this.decoded.email;
    
  }
  OnLogout() {
    this.service.logout();
  }
}
