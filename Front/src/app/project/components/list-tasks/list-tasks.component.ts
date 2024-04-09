import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AddTaskComponent } from '../add-task/add-task.component';
import {TaskService} from "../../../services/task.service";
import {TaskInter1} from "../../../interfaces/task-inter1";
import {NgToastService} from "ng-angular-popup";

@Component({
  selector: 'app-list-tasks',
  templateUrl: './list-tasks.component.html',
  styleUrls: ['./list-tasks.component.scss']
})
export class ListTasksComponent implements OnInit {
  arrTasks:any;

  displayedColumns: string[] = ['position', 'projectName', 'title', 'user', 'deadLineDate', 'status', 'actions'];
  //dataSource = ELEMENT_DATA;
  tasksFilter!: FormGroup
  users: any = [
    {name: "Moahmed", id: 1},
    {name: "Ali", id: 2},
    {name: "Ahmed", id: 3},
    {name: "Zain", id: 4},
  ]

  status: any = [
    {name: "Complete", id: 1},
    {name: "In-Prossing", id: 2},
  ]

  constructor(public dialog: MatDialog, private fb: FormBuilder,private  taskservice:TaskService,private toast: NgToastService) {
  }

  ngOnInit(): void {

    this.updateList();

    this.createform()
  }


  createform() {
    this.tasksFilter = this.fb.group({
      title: [''],
      userId: [''],
      fromDate: [''],
      toDate: ['']
    })
  }

  getAllTasks() {

  }

  updateList(){
    this.taskservice.getAllTasks().subscribe({
      next:value => this.arrTasks=value ,
      error :value=> this.toast.error({detail:"ERROR",summary:'ERROR while displaying tasks'+value,sticky:true})
    })
  }
  addTask() {
    const dialogRef = this.dialog.open(AddTaskComponent, {
      //width: '771px',
      width: '721px',
      height:'619px',

      panelClass: 'custom-dialog'
    });

    dialogRef.afterClosed().subscribe(result => {


       this.updateList();

    });
  }
}

