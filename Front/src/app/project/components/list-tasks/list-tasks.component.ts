import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
import { AddTaskComponent } from '../add-task/add-task.component';
import {TaskService} from "../../../services/task.service";
import {TaskInter1} from "../../../interfaces/task-inter1";
import {NgToastService} from "ng-angular-popup";
import {UserService} from "../../../services/user.service";
import {ProjectService} from "../../../services/project.service";
import {ConfirmationService} from "primeng/api";
import {UpdateTaskComponent} from "../update-task/update-task.component";
import {execute} from "@angular-devkit/build-angular/src/builders/karma";

@Component({
  selector: 'app-list-tasks',
  templateUrl: './list-tasks.component.html',
  styleUrls: ['./list-tasks.component.scss']
})
export class ListTasksComponent implements OnInit {
  arrTasks:any;

  displayedColumns: string[] = ['position','title', 'projectName',  'user', 'deadLineDate', 'status', 'actions'];

  tasksFilter!: FormGroup
  arrUsers: any;
  arrProjects:any;

  status: any = [
    {name: "Complete", id: 1},
    {name: "In-Prossing", id: 2},
  ]

  constructor(public dialog: MatDialog, private fb: FormBuilder,private confirmationService: ConfirmationService ,private  taskservice:TaskService,private srv:UserService,private srv2:ProjectService,private toast: NgToastService) {
  }

  ngOnInit(): void {

    this.updateList();

    this.srv.findAll().subscribe(
      {
        next: value =>{
          this.arrUsers=value;
        }
      }
    )
    this.srv2.findAll().subscribe({
      next: value =>{
        this.arrProjects=value;
      }
    })

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
      width: '696px',
      height: '552px',

      panelClass: 'custom-dialog'
    });

    dialogRef.afterClosed().subscribe(result => {


       this.updateList();

    });
  }

  deleteTask($event: MouseEvent,no:number) {
    console.log(no);
    this.confirmationService.confirm({
      target: $event.target as EventTarget,
      message: 'Do you want to delete this Task?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass:'bg-red-500 text-white p-2 rounded-md',
      rejectButtonStyleClass:"text-gray-700 p-2 rounded-md",
      acceptIcon:"none",
      rejectIcon:"none",

      accept: () => {

        this.taskservice.deleteTask(no).subscribe({

          next:value =>{
           this.updateList();
            this.toast.info({detail:"INFO",summary:'the Task is deleted',sticky:true});

          }
        });

      },
      reject: () => {
        this.toast.error({detail:"ERROR",summary:'Deletion Canceled',sticky:true});
      }
    });

  }

  updateTask1() {
    const dialogRef = this.dialog.open(AddTaskComponent, {
      //width: '771px',
      width: '691px',
      height:'582px',

      panelClass: 'custom-dialog'
    });

    dialogRef.afterClosed().subscribe(result => {


      this.updateList();

    });

  }
  updateTask(taskId: number) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '694px';
    dialogConfig.height = '582px';
    dialogConfig.panelClass = 'custom-dialog';
    dialogConfig.data = { taskId: taskId }; // Pass task id to the dialog

    const dialogRef = this.dialog.open(UpdateTaskComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.updateList();
    });
  }


  ert() {

  }
}

