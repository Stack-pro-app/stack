import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {UserService} from "../../../services/user.service";
import {MatDialog} from "@angular/material/dialog";
import {AddTaskComponent} from "../add-task/add-task.component";
import {ActivityComponent} from "../../userTask/activity/activity.component";
export interface PeriodicElement {
  name: string;
  email: string;
  tasksAssigned: string;
}


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  arrUsers: any;
  displayedColumns: string[] = ['position', 'UserName', 'Email' ,'tasksAssigned', 'actions'];
  taskId : any;
  //f1: FormGroup;
  constructor(private srv:UserService,public dialog: MatDialog) {
    /*this.f1 = fb.group({
      TaskId: ['', Validators.required],
    });*/

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




  act() {
    console.log(this.taskId);
  }
}
