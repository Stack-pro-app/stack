import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WorkspaceService } from '../../../core/services/Workspace/workspace.service';

@Component({
  selector: 'app-invitations',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './invitations.component.html',
  styleUrl: './invitations.component.css'
})
export class InvitationsComponent implements OnInit{
  invitations:any[] = [];
  invitaionsDtos:any[]=[];
  constructor(private workspaceService:WorkspaceService) {

  }
  ngOnInit(): void {
    this.onGetUserInvitaions( );
  }

  onGetUserInvitaions(){
    this.workspaceService.getUserSInvitions(localStorage.getItem('userId')).subscribe({
      next:(data)=>{
        this.invitations = data.result;

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
          console.log(this.invitaionsDtos);
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
        this.onGetUserInvitaions();
      },
      error:(err)=>{
        console.log(err);
      },
      complete:()=>{
        this.invitations = this.invitations.filter((inv)=>inv.WorkspaceId != invitaion.workspaceId);
        this.invitaionsDtos = this.invitaionsDtos.filter((inv)=>inv.id != invitaion.workspaceId);
      }
    })

  }
  onDeclineInvitation(invitaion:any){
    const invitation = this.invitations.filter((inv)=>inv.WorkspaceId == invitaion.workspaceId);

    this.workspaceService.onDeclineInvitation(invitation).subscribe({
      next:(data)=>{
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
