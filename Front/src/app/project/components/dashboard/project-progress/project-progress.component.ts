import {Component, OnInit} from '@angular/core';
import {AnimationModel} from "@syncfusion/ej2-angular-charts";

@Component({
  selector: 'app-project-progress',
  templateUrl: './project-progress.component.html',
  styleUrl: './project-progress.component.css'
})
export class ProjectProgressComponent  implements OnInit{
  public ARR:any=[];

  ngOnInit(): void {
    this.ARR=[{name:'imad'},{name:'smadi'}];

  }

}
