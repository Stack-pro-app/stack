import { Workspace } from './../../../core/Models/workspace';
import { WorkspaceService } from './../../../core/services/Workspace/workspace.service';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { StoreService } from '../../../core/services/store/store.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/Auth/auth.service';
import { ThemeSwitcherComponent } from '../../../shared/components/theme-switcher/theme-switcher.component';
import { InvitationsComponent } from '../invitations/invitations.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, CommonModule, ThemeSwitcherComponent,InvitationsComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  isLogged: Boolean = false;
  constructor(private store: StoreService
    ,private service:AuthService
,private router : Router) {}
  ngOnInit(): void {
    this.isLogged = this.store.isLogged();
  }
  OnLogout() {
    this.service.logout();
    this.router.navigate(['/Welcome']);

  }
}
