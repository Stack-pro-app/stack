import {Component, OnInit} from '@angular/core';
import {GantInter} from "../../../interfaces/Gant";
import {TaskService} from "../../../services/task.service";
import {Task} from "../../../interfaces/Gant/Task";
import {map, Observable} from "rxjs";
@Component({
  selector: 'app-gantt',
  templateUrl: './gantt.component.html',
  styleUrl: './gantt.component.css'
})
export class GanttComponent implements OnInit{
  public tmp?: GantInter[];
  public tmp2?:GantInter;
  public data$!: Observable<Task[]>;
  public taskSettings?: object;

  constructor(private  taskservice:TaskService) {
  }

  public ngOnInit(): void {


    this.data$ = this.taskservice.getGant().pipe(
      map(value => {
        const data: Task[] = [];
        for (const g of value) {
          const x: Task = {
            TaskID: g.id,
            TaskName: g.projectName,
            StartDate: new Date(g.start),
            EndDate: new Date(g.end),
          };
          x.subtasks = [];
          for (const h of g.tasks) {
            x.subtasks.push({
              TaskID: h.no,
              TaskName: h.title,
              StartDate: new Date(h.start),
              Progress: h.status,
              Duration: this.duration(h.start,h.end)
            });
          }
          data.push(x);
          console.log(x);
        }
        return data;
      })
    );


    this.taskSettings = {
      id: 'TaskID',
      name: 'TaskName',
      startDate: 'StartDate',
      endDate: 'EndDate',
      duration: 'Duration',
      progress: 'Progress',

      child: 'subtasks'
    };
  }
  public duration(start :string, end:string ){
    const startDate = new Date(start); // Example start date
    const endDate = new Date(end);   // Example end date

// Convert both dates to UTC to ensure accurate calculation
    const utcStartDate = Date.UTC(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());
    const utcEndDate = Date.UTC(endDate.getFullYear(), endDate.getMonth(), endDate.getDate());

// Calculate the difference in milliseconds
    const millisecondsDifference = Math.abs(utcEndDate - utcStartDate);

// Convert milliseconds to days
    const daysDifference = Math.floor(millisecondsDifference / (1000 * 60 * 60 * 24));

    return daysDifference +1 ;
  }


}
