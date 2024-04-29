import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProjectRoutingModule } from './project-routing.module';
import { PostProComponent } from './post-pro/post-pro.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatStepperModule} from "@angular/material/stepper";
import {MatButtonModule} from "@angular/material/button";

import {MAT_DATE_LOCALE, provideNativeDateAdapter} from "@angular/material/core";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {NgToastModule} from "ng-angular-popup";
import { DisplayProjectComponent } from './display-project/display-project.component';
import { EditProjectComponent } from './edit-project/edit-project.component';

import {ConfirmDialogModule} from "primeng/confirmdialog";
import {ButtonModule} from "primeng/button";
import {ToastModule} from "primeng/toast";
import {ConfirmationService, MessageService} from "primeng/api";
import {AddTaskComponent} from "./components/add-task/add-task.component";
import {ListTasksComponent} from "./components/list-tasks/list-tasks.component";
import {MatIconModule, MatIconRegistry} from "@angular/material/icon";
import {MatSelectModule} from "@angular/material/select";
import {
  MatCell,
  MatCellDef, MatColumnDef,
  MatHeaderCell,
  MatHeaderCellDef, MatHeaderRow,
  MatHeaderRowDef, MatRow,
  MatRowDef,
  MatTable
} from "@angular/material/table";
import {MatDialog, MatDialogClose} from "@angular/material/dialog";
import {MatMomentDateModule} from "@angular/material-moment-adapter";
import {UsersComponent} from "./components/users/users.component";
import {UpdateTaskComponent} from "./components/update-task/update-task.component";
import {
  CriticalPathService,
  EditService, ExcelExportService,
  GanttModule,
  PdfExportService, SelectionService,
  ToolbarService
} from "@syncfusion/ej2-angular-gantt";
import {Gantt1Component} from "./components/gantt/gantt1.component";
import { ListTaskComponent } from './userTask/list-task/list-task.component';
import { DetailTaskComponent } from './userTask/detail-task/detail-task.component';
import { StatisticsComponent } from './components/statistics/statistics.component';
import {CircularChart3DAllModule} from "@syncfusion/ej2-angular-charts";
import { ChartModule} from '@syncfusion/ej2-angular-charts';

import { CategoryService, BarSeriesService, ColumnSeriesService, LineSeriesService,LegendService, DataLabelService, MultiLevelLabelService} from '@syncfusion/ej2-angular-charts';
import { ActivityComponent } from './userTask/activity/activity.component';





import {
  AgendaService, DayService, ICalendarExportService, ICalendarImportService, MonthAgendaService,
  MonthService, PrintService, RecurrenceEditorModule,
  ScheduleModule, TimelineMonthService, TimelineViewsService,
  WeekService,
  WorkWeekService
} from "@syncfusion/ej2-angular-schedule";
import { Schedule0Component } from './components/schedule0/schedule0.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ProjectInfoComponent } from './components/dashboard/project-info/project-info.component';
import { ProjectProgressComponent } from './components/dashboard/project-progress/project-progress.component';
import { TasksInfoComponent } from './components/dashboard/tasks-info/tasks-info.component';
import {ProgressBarModule} from "primeng/progressbar";
import {CarouselModule} from "primeng/carousel";
import { ListUsersComponent } from './components/dashboard/list-users/list-users.component';
import {FloatLabelModule} from "primeng/floatlabel";
import { UserProgressComponent } from './components/dashboard/user-progress/user-progress.component';
import { ChartsComponent } from './components/dashboard/charts/charts.component';
import { Charts0Component } from './components/dashboard/charts0/charts0.component';







@NgModule({
  declarations: [
    PostProComponent,
    DisplayProjectComponent,
    EditProjectComponent,
    AddTaskComponent,
    ListTasksComponent,
    UsersComponent,
    UpdateTaskComponent,
    Gantt1Component,
    ListTaskComponent,
    DetailTaskComponent,
    StatisticsComponent,
    ActivityComponent,
    Schedule0Component,
    DashboardComponent,
    ProjectInfoComponent,
    ProjectProgressComponent,
    TasksInfoComponent,
    ListUsersComponent,
    UserProgressComponent,
    ChartsComponent,
    Charts0Component,






  ],
  imports: [
    CommonModule,
    ProjectRoutingModule,
    FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule,
    MatStepperModule, ReactiveFormsModule,
    MatDatepickerModule,
    NgToastModule,
    ConfirmDialogModule, ToastModule,
    ButtonModule,
    MatIconModule,
    MatSelectModule, MatTable, MatCellDef, MatHeaderCellDef, MatHeaderRowDef, MatRowDef, MatHeaderCell, MatColumnDef, MatCell, MatHeaderRow, MatRow, MatDialogClose,
    MatMomentDateModule,
    GanttModule,
    CircularChart3DAllModule,ChartModule,
    ScheduleModule,
    ProgressBarModule,
    RecurrenceEditorModule,
    CarouselModule,
    FloatLabelModule

  ],
  providers: [

    provideNativeDateAdapter(),
    ConfirmationService,
    MessageService,
    CriticalPathService, ToolbarService, EditService,SelectionService, ExcelExportService,CircularChart3DAllModule,
    CategoryService, BarSeriesService, ColumnSeriesService, LineSeriesService,LegendService, DataLabelService, MultiLevelLabelService, SelectionService,
    CategoryService, ColumnSeriesService, LineSeriesService,
    DayService, WeekService, WorkWeekService, MonthService, AgendaService, MonthAgendaService, TimelineViewsService, TimelineMonthService,
    ICalendarExportService,
    ICalendarImportService, PrintService,




  ]
})
export class ProjectModule { }
