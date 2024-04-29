import { Component } from '@angular/core';
import {UserService} from "../../../../services/user.service";
import {MatDialog} from "@angular/material/dialog";
import {NgToastService} from "ng-angular-popup";

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrl: './list-users.component.scss'
})
export class ListUsersComponent {
  arrUsers: any;
  displayedColumns: string[] = ['position', 'UserName', 'Email' ,'tasksAssigned', 'actions'];
  taskId : any;

  constructor(private srv:UserService) {

  }

  ngOnInit(): void {
    this.srv.getWithTsk().subscribe(
      {
        next: value =>{
          this.arrUsers=value;

        }
      }
    )
  }






}
