import {Component, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {FormBuilder} from "@angular/forms";
import {ConfirmationService} from "primeng/api";
import {TaskService} from "../../../../services/task.service";
import {UserService} from "../../../../services/user.service";
import {ProjectService} from "../../../../services/project.service";
import {NgToastService} from "ng-angular-popup";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-tasks-info',
  templateUrl: './tasks-info.component.html',
  styleUrl: './tasks-info.component.scss'
})
export class TasksInfoComponent implements OnInit  {
  arrTasks:any;
  displayedColumns: string[] = ['position','title', 'projectName',  'user', 'deadLineDate', 'status'];
  act1:ActivatedRoute  ;
  idAdmin :any;
  constructor(private act0:ActivatedRoute,private fb: FormBuilder,private  taskservice:TaskService) {
    this.act1=act0;
  }
  ngOnInit(): void {
    console.log(this.act1.paramMap.subscribe({
      next :value =>{
        this.idAdmin=value?.get('id');
        this.taskservice.getAllTasks00(this.idAdmin).subscribe({
          next:value => this.arrTasks=value ,

        })
      }
    }))


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
