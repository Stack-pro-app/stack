import { Workspace } from './../../../core/Models/workspace';
import { WorkspaceService } from './../../../core/services/Workspace/workspace.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { StoreService } from '../../../core/services/store/store.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/Auth/auth.service';
import { ThemeSwitcherComponent } from '../../../shared/components/theme-switcher/theme-switcher.component';
import { InvitationsComponent } from '../invitations/invitations.component';
import { Profile2Component } from '../profile-2/profile-2.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, CommonModule, ThemeSwitcherComponent,InvitationsComponent,Profile2Component],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  @Output() reloadWS = new EventEmitter<string>();
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
  onReloadWS(message:string){
    this.reloadWS.emit(message);
  }
}
