import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {range} from "rxjs";
import {ProjectService} from "../../../services/project.service";
import {NgToastService} from "ng-angular-popup";
import {UserService} from "../../../services/user.service";
import {TaskService} from "../../../services/task.service";
import {TaskInter} from "../../../interfaces/task-inter";
import {ProjectInter} from "../../../interfaces/project-inter";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.scss']
})
export class AddTaskComponent implements OnInit {
  f1: FormGroup;
  arrUsers: any;
  arrProjects:ProjectInter[]=[];
  task ?: TaskInter ;
  idAdmin:any ;


  constructor(@Inject(MAT_DIALOG_DATA) public data: any,private fb:FormBuilder , public dialog: MatDialogRef<AddTaskComponent> , public matDialog:MatDialog,private srv:UserService,private toast: NgToastService,private srv2:ProjectService,private  taskservice:TaskService) {

    this.f1 = fb.group({
      title: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]],
      descr: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(25)]],
      start : ['',Validators.required],
      end : ['',Validators.required],
      userName : ['',Validators.required],
      projectName:['',Validators.required],

    });
    this.idAdmin = data.idAdmin;
    console.log(this.idAdmin);
  }
  ngOnInit(): void {


    this.srv.findAll(this.idAdmin).subscribe(
      {
        next: value =>{
          this.arrUsers=value;
        }
      }
    )
    this.srv2.getAdminProjects(this.idAdmin).subscribe({
      next: value =>{
        this.arrProjects=value;
      }
    })


  }

  protected readonly range = range;

  createTask( ) {


     if(!this.f1.invalid){
         this.srv.sameWork(this.f1.get("projectName")?.value,this.f1.get("userName")?.value).subscribe({
           next:value => {
             this.srv2.findById(this.f1.get("projectName")?.value).subscribe({
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
                   this.task={
                     title : this.f1.get("title")?.value,
                     description : this.f1.get("descr")?.value,
                     projectId : this.f1.get("projectName")?.value,
                     userId: this.f1.get("userName")?.value,
                     start:this.f1.get("start")?.value,
                     end:this.f1.get("end")?.value,
                     status:0
                   }

                   this.taskservice.createTask(this.task).subscribe(
                     {
                       next:value => {this.toast.success({detail:"SUCCESS",summary:'Task added with success'});
                         this.f1.reset();
                       },
                       error :value=> this.toast.error({detail:"ERROR",summary:'NOT created '+value,sticky:true})
                     }
                   )
                 }


               },
               error :value=> this.toast.error({detail:"ERROR",summary:'NOT Created '+value,sticky:true})
             })
           },
           error : value=> this.toast.error({detail:"ERROR",summary:'the user and the project are not in the same workspace',sticky:true})
         })




     }
     else{
       this.toast.error({detail:"ERROR",summary:'Complete all the information',sticky:true});
     }



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
