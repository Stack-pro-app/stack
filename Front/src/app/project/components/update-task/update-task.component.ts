import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {TaskInter} from "../../../interfaces/task-inter";
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {UserService} from "../../../services/user.service";
import {NgToastService} from "ng-angular-popup";
import {ProjectService} from "../../../services/project.service";
import {TaskService} from "../../../services/task.service";
import {AddTaskComponent} from "../add-task/add-task.component";
import {TaskInter1} from "../../../interfaces/task-inter1";
import {forkJoin} from "rxjs";

@Component({
  selector: 'app-update-task',
  templateUrl: './update-task.component.html',
  styleUrl: './update-task.component.css'
})
export class UpdateTaskComponent implements OnInit {
  f1: FormGroup;
  arrUsers: any;
  arrProjects:any;
  task ?: TaskInter1 ;
  id ?: number ;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,private fb:FormBuilder , public dialog: MatDialogRef<AddTaskComponent> , public matDialog:MatDialog,private srv:UserService,private toast: NgToastService,private srv2:ProjectService,private  taskservice:TaskService) {
    this.f1 = fb.group({
      title: ['', Validators.required],
      descr: ['', Validators.required],
      start : ['',Validators.required],
      end : ['',Validators.required],
      userName : ['',Validators.required],
      projectName:['',Validators.required],

    });

    this.id=data.taskId;
  }

  ngOnInit(): void {
    this.srv.findAll().subscribe(
      {
        next: value =>{
          this.arrUsers=value;
        }
      }
    );
    this.srv2.findAll().subscribe({
      next: value =>{
        this.arrProjects=value;
      }
    });

    this.taskservice.getByNo(this.id).subscribe({
      next:value => {this.task=value;console.log(this.task);this.populateForm()},
      error:value => console.log("ERROR")
    })



  }


  UpdatTask() {
    let idProject: any;
    let idUser: any;

    const project$ = this.srv2.IdByName(this.f1.get("projectName")?.value);
    const user$ = this.srv.IdByName(this.f1.get("userName")?.value);

    forkJoin([project$, user$]).subscribe({
      next: ([projectId, userId]) => {
        idProject = projectId;
        idUser = userId;

        const task: TaskInter = {
          no: this.task?.no,
          title: this.f1.get("title")?.value,
          description: this.f1.get("descr")?.value,
          projectId: idProject,
          userId: idUser,
          start: this.f1.get("start")?.value,
          end: this.f1.get("end")?.value,
          status:this.task?.status

        };

        console.log(task);

        this.taskservice.createTask(task).subscribe({
          next: value => this.toast.success({ detail: "SUCCESS", summary: 'Task updated with success' }),
          error: value => this.toast.error({ detail: "ERROR", summary: 'NOT updated' + value, sticky: true })
        });
      },
      error: error => console.error('Error fetching project and user IDs:', error)
    });
  }
  populateForm(): void {

      this.f1.patchValue({
        title:this.task?.title,
        descr:this.task?.description,
        end:this.task?.end,
        userName:this.task?.userName,
        projectName:this.task?.projectName,
        start : this.task?.start





      });

    }

}
