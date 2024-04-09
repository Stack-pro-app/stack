import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProjectRoutingModule } from './project-routing.module';
import { PostProComponent } from './post-pro/post-pro.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
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
import {MatIconModule} from "@angular/material/icon";
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






@NgModule({
  declarations: [
    PostProComponent,
    DisplayProjectComponent,
    EditProjectComponent,
    AddTaskComponent,
    ListTasksComponent

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
    MatMomentDateModule




    // RouterModule, ToastModule, ConfirmDialogModule

  ],
  providers: [
    provideNativeDateAdapter(),
    ConfirmationService,
    MessageService,




  ]
})
export class ProjectModule { }