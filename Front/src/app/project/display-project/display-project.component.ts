import {Component, OnInit} from '@angular/core';

import {NgToastService} from "ng-angular-popup";
import {ProjectService} from "../../services/project.service";


import {ConfirmationService} from "primeng/api";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";

import {EditProjectComponent} from "../edit-project/edit-project.component";
import {ProjectInter} from "../../interfaces/project-inter";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-display-project',
  templateUrl: './display-project.component.html',
  styleUrl: './display-project.component.scss'
})
export class DisplayProjectComponent implements OnInit {
  empty: boolean = false;
  arr: ProjectInter[]=[];
  act1:ActivatedRoute  ;
  id:any

  constructor(private act0:ActivatedRoute,public dialog: MatDialog, private srv: ProjectService, private toast: NgToastService, private confirmationService: ConfirmationService) {
    this.act1=act0;
  }

  ngOnInit(): void {
    console.log(this.act1.paramMap.subscribe({
        next :(value)=>{console.log(value);this.id=value?.get('id');console.log(this.id)},
      }
    ));
    this.srv.getAdminProjects(this.id).subscribe({
      next: value => {
        this.arr = value;
        if (this.arr.length != 0) {
          this.empty = true;
        }
      }
    })

  }


  confirm2($event: MouseEvent, id: any, index: any) {

    this.confirmationService.confirm({
      target: $event.target as EventTarget,
      message: 'All tasks of the projects will be deleted!!',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'bg-red-500 text-white p-2 rounded-md',
      rejectButtonStyleClass: "text-gray-700 p-2 rounded-md",
      acceptIcon: "none",
      rejectIcon: "none",

      accept: () => {

        this.srv.deleteByid(id).subscribe({

          next: value => {
            this.arr.splice(index, 1);
            this.toast.info({detail: "INFO", summary: 'the project is deleted', sticky: true});

          }
        });

      },
      reject: () => {
        this.toast.error({detail: "ERROR", summary: 'Deletion Canceled', sticky: true});
      }
    });
  }

  /*updatePro(taskId: number) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '694px';
    dialogConfig.height = '582px';
    dialogConfig.panelClass = 'custom-dialog';
    dialogConfig.data = {taskId: taskId}; // Pass task id to the dialog

    const dialogRef = this.dialog.open(EditProjectComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {

    });


  }*/
}
