import {Component, OnInit} from '@angular/core';
import {AnimationModel} from "@syncfusion/ej2-angular-charts";
import {TaskService} from "../../../../services/task.service";
import {GantInter} from "../../../../interfaces/Gant";
import {projectProgress} from "../../../../interfaces/projetProgress";

@Component({
  selector: 'app-project-progress',
  templateUrl: './project-progress.component.html',
  styleUrl: './project-progress.component.css'
})
export class ProjectProgressComponent  implements OnInit{
  public ARR?:GantInter[];
  public ARR2?:projectProgress[];


  constructor(private  taskservice:TaskService) {
  }

  ngOnInit(): void {
    this.taskservice.getGant().subscribe({
      next:value => {
        this.ARR=value;
        this.ARR2 = [];
        for(let g of this.ARR){
          let prj : projectProgress ={projectName:g.projectName,toDo:0,inProgress:0,missed:0,done:0,progress:0};
          for(let ts of g.tasks){
            prj.progress+=ts.status ;
            if(ts.status==100){
              prj.done++;
            } else if (this.isBeforeToday(ts.start)){
              prj.toDo++;
            } else if(this.isAfterDead(ts.end)){
              prj.missed++;
            } else{
              prj.inProgress++;
            }
          }
          let totalTasks =prj.inProgress+prj.done+prj.missed;

          if(totalTasks!=0){
            prj.progress = prj.progress/totalTasks;
            prj.progress = Number(prj.progress.toFixed(2));
          }

          this.ARR2.push(prj);
        }
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
  isAfterDead(endDateStr: string): boolean {
    const today = new Date();
    const endDate = new Date(endDateStr);



    today.setHours(0, 0, 0, 0);
    endDate.setHours(0, 0, 0, 0);

    return endDate < today;
  }




}
