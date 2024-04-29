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
import {ProjectInter} from "../../../interfaces/project-inter";

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
      title: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]],
      descr: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(25)]],
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


    if(!this.f1.invalid){
      let idProject: any;
      let idUser: any;

      const project$ = this.srv2.IdByName(this.f1.get("projectName")?.value);
      const user$ = this.srv.IdByName(this.f1.get("userName")?.value);

      forkJoin([project$, user$]).subscribe({
        next: ([projectId, userId]) => {
          idProject = projectId;
          idUser = userId;
          this.srv2.findProName(this.f1.get("projectName")?.value).subscribe({
            next :value => {
              const prj : ProjectInter = value ;
              if (this.isAfterProjectEnd(this.f1.get("end")?.value,this.f1.get("start")?.value)){

                this.toast.error({detail:"ERROR",summary:'The Start Date Must Before The End Date  ',sticky:true});
              }
              else if(this.isBeforeProjectStart(prj.start,this.f1.get("start")?.value)){
                this.toast.error({detail:"ERROR",summary:'The Task Can Not Start Before '+prj.start,sticky:true});
              }
              else if (this.isAfterProjectEnd(prj.end,this.f1.get("end")?.value)){
                this.toast.error({detail:"ERROR",summary:'The Task Can Not End  After '+prj.end,sticky:true})

              }
              else{
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


                this.taskservice.createTask(task).subscribe({
                  next: value => this.toast.success({ detail: "SUCCESS", summary: 'Task updated with success' }),
                  error: value => this.toast.error({ detail: "ERROR", summary: 'NOT updated' + value, sticky: true })
                });
              }


            }});

        },
        error: error => console.error('Error fetching project and user IDs:', error)
      });

    }
    else{
      this.toast.error({detail:"ERROR",summary:'Complete all the information',sticky:true});
    }



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
  isBeforeProjectStart(dateStr: string,startTask:string): boolean {
    const Task = new Date(startTask);
    const date = new Date(dateStr);


    Task.setHours(0, 0, 0, 0);
    date.setHours(0, 0, 0, 0);

    return Task <  date;
  }
  isAfterProjectEnd(dateStr: string,startTask:string): boolean {
    const Task = new Date(startTask);
    const date = new Date(dateStr);


    Task.setHours(0, 0, 0, 0);
    date.setHours(0, 0, 0, 0);

    return Task >  date;
  }

}
