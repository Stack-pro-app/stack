import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { StoreService } from '../../../core/services/store/store.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/Auth/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  isLogged: Boolean = false;
  constructor(private store: StoreService
    ,private service:AuthService) {}
  ngOnInit(): void {
    this.isLogged = this.store.isLogged();
  }
  OnLogout() {
    this.service.logout();
  }
}
