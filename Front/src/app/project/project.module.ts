import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProjectRoutingModule } from './project-routing.module';
import { PostProComponent } from './post-pro/post-pro.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatStepperModule} from "@angular/material/stepper";
import {MatButtonModule} from "@angular/material/button";

import {provideNativeDateAdapter} from "@angular/material/core";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {NgToastModule} from "ng-angular-popup";
import { DisplayProjectComponent } from './display-project/display-project.component';
import { EditProjectComponent } from './edit-project/edit-project.component';
import {RouterModule} from "@angular/router";

//import {ConfirmationService, MessageService} from "primeng/api";
//import {ToastModule} from "primeng/toast";
//import {ConfirmDialogModule} from "primeng/confirmdialog";




@NgModule({
  declarations: [
    PostProComponent,
    DisplayProjectComponent,
    EditProjectComponent
  ],
  imports: [
    CommonModule,
    ProjectRoutingModule,
    FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule,
    MatStepperModule, ReactiveFormsModule,
    MatDatepickerModule,
    NgToastModule,

   // RouterModule, ToastModule, ConfirmDialogModule

  ],
  providers: [
    provideNativeDateAdapter(),
   // ConfirmationService,
   // MessageService

  ]
})
export class ProjectModule { }
