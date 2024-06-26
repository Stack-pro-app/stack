import {Component, OnInit} from '@angular/core';

import {TaskService} from "../../../services/task.service";
import {map, Observable} from "rxjs";

import {Statis} from "../../../interfaces/Gant/Statis";


@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrl: './statistics.component.css'
})
export class StatisticsComponent implements OnInit{
  public primaryXAxis?: Object;
  public primaryYAxis?: Object;




  public dataSource?: Statis[];
  public title?: string;
  public legendSettings?: Object;
  public dataLabel?: Object;


  public data$!: Observable<Statis[]>;
  public tmp: Statis={} ;

  public chartData?: Object[];


  constructor(private  taskservice:TaskService) {
  }

  ngOnInit(): void {


    this.data$ = this.taskservice.getGant().pipe(
      map(value => {
        const dt : Statis[] =[];
        for (const g of value) {
          const tmp: Statis = {
            x:g.projectName,
            y:0
          };
          let st :number=0;
          let i = 0  ;
          for (const h of g.tasks) {

            if(!this.isBeforeToday(h.start)){
              st+=h.status;
              i++;
            }

          }
          if(i!=0){
            tmp.y=st/i;
          }

          dt.push(tmp);
          this.chartData?.push({})
        }
        return dt ;
      })
    );
    this.primaryXAxis = {
      valueType: 'Category',
      title: 'Projects'
    };
    this.primaryYAxis = {
      minimum: 0, maximum: 100,
      interval: 20, title: 'Progress %'
    };

    this.title = 'the overall progress of the projects';
    this.legendSettings = { visible: true, position: 'Right' };
    this.dataLabel = {
      visible: true,
      name: 'x',
      position: 'Outside',
      font: {
        fontWeight: '600'
      },
      connectorStyle: { length: '40px' }
    };
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
