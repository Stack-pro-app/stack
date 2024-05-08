import { Workspace } from './../../../core/Models/workspace';
import { WorkspaceService } from './../../../core/services/Workspace/workspace.service';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { StoreService } from '../../../core/services/store/store.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/Auth/auth.service';
import { ThemeSwitcherComponent } from '../../../shared/components/theme-switcher/theme-switcher.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, CommonModule, ThemeSwitcherComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  invitations:any[] = [];
  invitaionsDtos:any[]=[];
  isLogged: Boolean = false;
  constructor(private store: StoreService
    ,private service:AuthService,
  private workspaceService:WorkspaceService
,private router : Router) {}
  ngOnInit(): void {
    this.isLogged = this.store.isLogged();
    this.onGetUserInvitaions( );
  }
  OnLogout() {
    this.service.logout();
    this.router.navigate(['/Welcome']);

  }
  onGetUserInvitaions(){
    this.workspaceService.getUserSInvitions(localStorage.getItem('userId')).subscribe({
      next:(data)=>{
        this.invitations = data.result;
        console.log(this.invitations);

      },
      error:(err)=>{
        console.log(err);
      },
      complete:()=>{this.onGetInvitaionDto();
      }
    })
  }
  onGetInvitaionDto(){
    for(let invitation of this.invitations){
      this.workspaceService.getWorkspace(invitation.workspaceId,localStorage.getItem('userId')).subscribe({
        next:(data)=>{
          this.invitaionsDtos.push(data.result);
        },
        error:(err)=>{
          console.log(err);
        },
        complete:()=>{console.log("completed");
        }
      })
    }
  }

  onAceptInvitaion(invitaion:any){
    const invitation = this.invitations.filter((inv)=>inv.WorkspaceId == invitaion.workspaceId);

    this.workspaceService.onAcceptInvitation(invitation).subscribe({
      next:(data)=>{
        console.log(data);
        this.onGetUserInvitaions();
      },
      error:(err)=>{
        console.log(err);
      },
      complete:()=>{console.log("completed");
      }
    })

  }
  onDeclineInvitation(invitaion:any){
    const invitation = this.invitations.filter((inv)=>inv.WorkspaceId == invitaion.workspaceId);

    this.workspaceService.onDeclineInvitation(invitation).subscribe({
      next:(data)=>{
        console.log(data);
        this.onGetUserInvitaions();
        window.location.reload();

      },
      error:(err)=>{
        console.log(err);
      },
      complete:()=>{console.log("completed");
      }
    })
  }
}
