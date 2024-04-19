import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {TaskService} from "../../../services/task.service";
import {TaskInter1} from "../../../interfaces/task-inter1";
import {NgToastService} from "ng-angular-popup";


@Component({
  selector: 'app-detail-task',
  templateUrl: './detail-task.component.html',
  styleUrl: './detail-task.component.css'
})
export class DetailTaskComponent implements OnInit{
  id ?: number ;
  task : TaskInter1={start:''} ;
  done: boolean=false;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,private  taskservice:TaskService,private toast: NgToastService) {
    this.id=data.taskId;

  }

  ngOnInit(): void {

    this.taskservice.getByNo(this.id).subscribe({
      next:value => {
        this.task.title=value.title;
        this.task.description=value.description;
        this.task.start=value.start;
        this.task.end=value.end;
        this.task.projectName=value.projectName;
        this.task.status=value.status;
        this.done=(value.status==100 || this.isBeforeToday(value.start))

      },
      error:value => console.log("ERROR")
    })
  }



  updateTAsk() {
    console.log(this.task.status);
    this.taskservice.updateProgrss(this.id, this.task.status).subscribe({
      next: value => {
        this.toast.success({detail: "SUCCESS", summary: 'Progress Updated !!!!'});
      }
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

}
