import {Component, OnInit, ViewChild} from '@angular/core';
import {GantInter} from "../../../interfaces/Gant";
import {TaskService} from "../../../services/task.service";
import {Task} from "../../../interfaces/Gant/Task";
import {map, Observable} from "rxjs";
import {EditSettingsModel, PdfExport, ToolbarItem} from "@syncfusion/ej2-angular-gantt";
import { GanttComponent } from '@syncfusion/ej2-angular-gantt';

import {MatDialog} from "@angular/material/dialog";
import {AddTaskComponent} from "../add-task/add-task.component";
import {StatisticsComponent} from "../statistics/statistics.component";
@Component({
  selector: 'app-gantt',
  templateUrl: './gantt.component.html',
  styleUrl: './gantt.component.css'
})
export class Gantt1Component implements OnInit{

  public tmp?: GantInter[];
  public data$!: Observable<Task[]>;
  public taskSettings?: object;
  critical : boolean =false ;
  public toolbar?: ToolbarItem[];
  public pdfExportInstance?: PdfExport;


  public editSettings?: EditSettingsModel;
  public labelSettings?: object;

  @ViewChild('ganttObject')
  public ganttObject: GanttComponent|undefined;


  constructor(private  taskservice:TaskService,public dialog: MatDialog) {
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
              Duration: this.duration(h.start,h.end),

            });
          }
          data.push(x);
          console.log(x);
        }
        return data;
      })
    );
    this.labelSettings = {
      leftLabel: 'TaskName',
      taskLabel: 'Progress'
    };


    this.taskSettings = {
      id: 'TaskID',
      name: 'TaskName',
      startDate: 'StartDate',
      endDate: 'EndDate',
      duration: 'Duration',
      progress: 'Progress',
      child: 'subtasks'
    };


    this.editSettings = {
      allowAdding: true,
      allowTaskbarEditing: true,

    };

    this.toolbar = ['Add','ExpandAll', 'CollapseAll','CriticalPath','Search', 'ZoomIn', 'ZoomOut', "ExcelExport", "CsvExport",];




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

  public toolbarClick(args: any): void {
   if(args.item.text === "Excel export"){
      (this.ganttObject as GanttComponent).excelExport({
        fileName: "ProjectData.xlsx",
        theme: {
          header: { fontColor:"#C67878"},
          record: { fontColor:"#C67878"}
        },
        header:{
          headerRows: 1,
          rows: [{
            cells:[{
              colSpan: 4,
              value: "Project Time Tracking Report",
              style: { fontSize:20, hAlign:"Center"}
            }]
          }]
        },
        footer:{
          footerRows: 1,
          rows: [{
            cells:[{
              colSpan: 4,

              style: { fontSize:18, hAlign:"Center"}
            }]
          }]
        }
      });
    } else if(args.item.text === "CSV export"){
      (this.ganttObject as GanttComponent).csvExport();
    }
  }


}
