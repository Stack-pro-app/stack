import { Component } from '@angular/core';
import {UserService} from "../../../../services/user.service";
import {MatDialog} from "@angular/material/dialog";
import {NgToastService} from "ng-angular-popup";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrl: './list-users.component.scss'
})
export class ListUsersComponent {
  arrUsers: any;
  displayedColumns: string[] = ['position', 'UserName', 'Email' ,'tasksAssigned', 'actions'];
  taskId : any;
  act1:ActivatedRoute  ;
  idAdmin :any;

  constructor(private act0:ActivatedRoute,private srv:UserService) {
    this.act1=act0;
  }

  ngOnInit(): void {
    console.log(this.act1.paramMap.subscribe({
      next :value =>{
        this.idAdmin=value?.get('id');
        this.srv.getWithTsk90(this.idAdmin).subscribe(
          {
            next: value =>{
              this.arrUsers=value;

            }
          }
        )

      }}))




  }






}
