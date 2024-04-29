import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { UserService } from "../../../services/user.service";
import { ActivityInter } from "../../../interfaces/Activity-inter";

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})
export class ActivityComponent implements OnInit {
  public primaryXAxis?: Object;
  public chartData?: Object[];
  public title?: string;
  public majorGridLines?: Object;
  public primaryYAxis?: Object;
  public lineStyle?: Object;
  public marker?: Object;
  public rows?: Object;



  public activity: ActivityInter[] = [];

  constructor(private route: ActivatedRoute, private userService: UserService) {}

  userId?: string;
  taskId?: string;

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.userId = params['userId'];
      this.taskId = params['taskId'];
      this.getData(this.userId, this.taskId);
    });

    this.primaryXAxis = {
      title: 'Date',
      valueType: 'Category',
      interval: 1
    };
    this.primaryYAxis = {
      minimum: 0, maximum: 90, interval: 20,
      lineStyle: { width: 0 },
      title: 'Progress',
      labelFormat: '{value}%'
    };
    this.majorGridLines = { width: 0};
    this.lineStyle = { width: 0};
    this.marker = {
      visible: true, width: 10, height: 10, border: { width: 2, color: '#F8AB1D' }
    }

  }

  getData(userId: any, taskId: any): void {
    this.userService.getActivity(userId, taskId).subscribe({
      next: value => {
        this.activity = value;
        this.title = 'Activity of '+this.activity[0].userName+' in  the Task : '+this.activity[0].taskName;
        this.chartData = this.activity.map(a => ({
          x: new Date(a.date), // Parse the date string into a Date object
          y: a.status,
          y1:a.status
        }));
      }
    });
  }
}

