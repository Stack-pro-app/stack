import {Component, OnInit} from '@angular/core';
import {Statis} from "../../../../interfaces/Gant/Statis";
import {map, Observable} from "rxjs";
import {TaskService} from "../../../../services/task.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-charts0',
  templateUrl: './charts0.component.html',
  styleUrl: './charts0.component.css'
})
export class Charts0Component implements OnInit{
  public primaryXAxis?: Object;
  public primaryYAxis?: Object;
  act1:ActivatedRoute  ;
  idAdmin :any;




  public dataSource?: Statis[];
  public title?: string;
  public legendSettings?: Object;
  public dataLabel?: Object;


  public data$!: Observable<Statis[]>;
  public tmp: Statis={} ;

  public chartData?: Object[];


  constructor(private act0:ActivatedRoute,private  taskservice:TaskService) {
    this.act1=act0;
  }

  ngOnInit(): void {

    console.log(this.act1.paramMap.subscribe({
      next :value =>{
        this.idAdmin=value?.get('id');
        this.data$ = this.taskservice.getGant0(this.idAdmin).pipe(
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




      }}))

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
