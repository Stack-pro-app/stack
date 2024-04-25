import {Component, ViewChild} from '@angular/core';
import {
  ActionEventArgs,
  EventSettingsModel,
  ScheduleComponent,
  ToolbarActionArgs
} from "@syncfusion/ej2-angular-schedule";
import { DataManager, UrlAdaptor } from '@syncfusion/ej2-data';
import { ItemModel } from '@syncfusion/ej2-angular-navigations';

@Component({
  selector: 'app-schedule0',
  templateUrl: './schedule0.component.html',
  styleUrl: './schedule0.component.css'
})
export class Schedule0Component {
  @ViewChild('sh')
  shObj!:ScheduleComponent

  private dataManager: DataManager = new DataManager({
    url: 'http://localhost:8080/schedule/load', // 'controller/actions'
    crudUrl: 'http://localhost:8080/schedule/update',
    adaptor: new UrlAdaptor,
    crossDomain:true
  });

  public data: object[] = [{
    Id: 1,
    Subject: 'imad',
    StartTime: new Date(2024, 3, 25, 10, 0),
    EndTime: new Date(2024, 3, 25, 12, 30),
    isAllDay: false
  }
  ];
  public eventSettings: EventSettingsModel = {
    dataSource: this.dataManager
  }

  onActionBegin(args:ToolbarActionArgs) {
    if(args.requestType==="toolbarItemRendering"){
      let exportItems:ItemModel ={
        text : 'Export Calendar',
        click :this.onExportClick.bind(this)
      }
      args.items?.push(exportItems);

    }

  }
  public onExportClick(){
  this.shObj.exportToICalendar();
  }
}
