import {Component, OnInit} from '@angular/core';
import {TaskService} from "../../../../services/task.service";
import {GantInter} from "../../../../interfaces/Gant";
import {projectProgress} from "../../../../interfaces/projetProgress";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-project-info',
  templateUrl: './project-info.component.html',
  styleUrl: './project-info.component.css'
})
export class ProjectInfoComponent implements OnInit {
  public ARR?: GantInter[];
  public ARR2: projectProgress[] = [];
  arr: string[] = [];
  responsiveOptions: any[] | undefined;
  act1:ActivatedRoute  ;
  idAdmin :any;

  constructor(private act0:ActivatedRoute,private taskservice: TaskService) {
    this.act1=act0;
  }

  ngOnInit(): void {
    console.log(this.act1.paramMap.subscribe({
      next :value =>{
        this.idAdmin=value?.get('id');
        this.taskservice.getGant0(this.idAdmin).subscribe({
          next: (value) => {
            this.ARR = value;
            this.ARR2 = [];
            for (let g of this.ARR) {
              let prj: projectProgress = {
                projectName: g.projectName,
                toDo: 0,
                inProgress: 0,
                missed: 0,
                done: 0,
                progress: 0,
              };
              for (let ts of g.tasks) {
                prj.progress += ts.status;
                if (ts.status == 100) {
                  prj.done++;
                } else if (this.isBeforeToday(ts.start)) {
                  prj.toDo++;
                } else if (this.isAfterDead(ts.end)) {
                  prj.missed++;
                } else {
                  prj.inProgress++;
                }
              }
              let totalTasks =
                prj.toDo + prj.inProgress + prj.done + prj.missed;

              if (totalTasks != 0) {
                prj.progress = prj.progress / (prj.toDo + prj.inProgress + prj.done + prj.missed);
                prj.progress = Number(prj.progress.toFixed(2));
              }

              this.ARR2.push(prj);
            }
          },
        });

      }}))

    this.responsiveOptions = [
      {
        breakpoint: '500px',
        numVisible: 1,
        numScroll: 1,
      },
      {
        breakpoint: '991px',
        numVisible: 2,
        numScroll: 1,
      },
      {
        breakpoint: '767px',
        numVisible: 1,
        numScroll: 1,
      },
    ];
  }

  isBeforeToday(dateStr: string): boolean {
    const today = new Date();
    const date = new Date(dateStr);

    today.setHours(0, 0, 0, 0);
    date.setHours(0, 0, 0, 0);

    return date > today;
  }

  isAfterDead(endDateStr: string): boolean {
    const today = new Date();
    const endDate = new Date(endDateStr);

    today.setHours(0, 0, 0, 0);
    endDate.setHours(0, 0, 0, 0);

    return endDate < today;
  }

  getSeverity(status: string): string {
    // Define your logic to determine severity based on status
    // This is just a placeholder, replace it with your actual logic
    if (status === 'In Progress') {
      return 'warn';
    } else if (status === 'Done') {
      return 'success';
    } else if (status === 'Not Completed') {
      return 'danger';
    } else {
      return 'info';
    }
  }
}
