import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import {range} from "rxjs";
import {ProjectService} from "../../../services/project.service";
import {NgToastService} from "ng-angular-popup";
import {UserService} from "../../../services/user.service";
import {TaskService} from "../../../services/task.service";
import {TaskInter} from "../../../interfaces/task-inter";

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.scss']
})
export class AddTaskComponent implements OnInit {
  f1: FormGroup;
  arrUsers: any;
  arrProjects:any;
  task ?: TaskInter ;

  constructor(private fb:FormBuilder , public dialog: MatDialogRef<AddTaskComponent> , public matDialog:MatDialog,private srv:UserService,private toast: NgToastService,private srv2:ProjectService,private  taskservice:TaskService) {
    this.f1 = fb.group({
      title: ['', Validators.required],
      descr: ['', Validators.required],
      start : ['',Validators.required],
      end : ['',Validators.required],
      userName : ['',Validators.required],
      projectName:['',Validators.required],

    });
  }
  ngOnInit(): void {

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

  }

  protected readonly range = range;

  createTask( ) {

     if(!this.f1.invalid){

       this.task={
         title : this.f1.get("title")?.value,
         description : this.f1.get("descr")?.value,
         projectId : this.f1.get("projectName")?.value,
         userId: this.f1.get("userName")?.value,
         start:this.f1.get("start")?.value,
         end:this.f1.get("end")?.value,
         status:0
       }
       console.log(this.task);
       this.taskservice.createTask(this.task).subscribe(
         {
           next:value => this.toast.success({detail:"SUCCESS",summary:'Task added with success'}),
             error :value=> this.toast.error({detail:"ERROR",summary:'NOT created '+value,sticky:true})
         }
       )
     }
     else{
       this.toast.error({detail:"ERROR",summary:'Complete all the information',sticky:true});
     }



  }

}
