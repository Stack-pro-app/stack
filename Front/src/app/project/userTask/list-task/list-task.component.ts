import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../services/user.service";
import {TaskInter1} from "../../../interfaces/task-inter1";
import {ActivatedRoute, Router} from "@angular/router";
import {TaskService} from "../../../services/task.service";
import {NgToastService} from "ng-angular-popup";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {AddTaskComponent} from "../../components/add-task/add-task.component";
import {DetailTaskComponent} from "../detail-task/detail-task.component";

@Component({
  selector: 'app-list-task',
  templateUrl: './list-task.component.html',
  styleUrl: './list-task.component.css'
})
export class ListTaskComponent implements  OnInit{
tasks?: TaskInter1[];

  id:any ;



  constructor(public dialog: MatDialog,private userService : UserService,private act0:ActivatedRoute,private taskService:TaskService,private toast: NgToastService,private router: Router) {

  }

  ngOnInit(): void {

    this.getTasks();
    this.userService.getTasks(this.id).subscribe({
      next:value => {
        this.tasks=value.tasks;
        let c : number=0 ;
        if(this.tasks){

          for (let t  of this.tasks){
            if(t.status!=100){
              c++;
            }
          }
        }
        if(c!=0){
          this.toast.info({detail:"INFO",summary:'U have '+c+" tasks not completed"});
        }
        else{
          this.toast.info({detail:"INFO",summary:'All Tasks are Completed'});
        }
      }
    });
  }
  getTsks23(){
    this.userService.getTasks(this.id).subscribe({
      next:value => {
        this.tasks=value.tasks;}



    });
  }

  done(no:any) {
    this.taskService.updateProgrss(no,100).subscribe({
      next:value => {
        this.toast.success({detail:"SUCCESS",summary:'Task Marked Done'});
       /* this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate([this.router.url]);
        });*/
        },
      error :value=> this.toast.error({detail:"ERROR",summary:'NOT updated '+value,sticky:true})
    }

  )
  }
  getTasks(){
    console.log(this.act0.paramMap.subscribe({
        next :(value)=>{console.log(value);this.id=value?.get('id');
          },
      }
    ));
  }

  detail(taskId:any) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '694px';
    dialogConfig.height = '582px';
    dialogConfig.panelClass = 'custom-dialog';
    dialogConfig.data = { taskId: taskId };

    const dialogRef = this.dialog.open(DetailTaskComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
       this.getTsks23();
    });

  }

  isBeforeToday(dateStr: string): boolean {
    const today = new Date();
    const date = new Date(dateStr);


    today.setHours(0, 0, 0, 0);
    date.setHours(0, 0, 0, 0);
   /* console.log("date"+date);
    console.log("today"+today);
    console.log(date >= today);*/
    return date > today;
  }
  isAfterDead(endDateStr: string): boolean {
    const today = new Date();
    const endDate = new Date(endDateStr);



    today.setHours(0, 0, 0, 0);
   endDate.setHours(0, 0, 0, 0);

    return endDate < today;
  }

}
