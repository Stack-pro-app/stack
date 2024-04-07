import {Component, OnInit} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {NgToastService} from "ng-angular-popup";
import {ProjectService} from "../../services/project.service";
import {ProjectInter} from "../../interfaces/project-inter";
//import {ConfirmationService} from "primeng/api";

@Component({
  selector: 'app-display-project',
  templateUrl: './display-project.component.html',
  styleUrl: './display-project.component.scss'
})
export class DisplayProjectComponent implements OnInit{
  empty:boolean=false;
  arr: any;

  constructor(private srv:ProjectService,private toast: NgToastService) {

  }

  ngOnInit(): void {
   this.srv.findAll().subscribe({
     next: value =>{
       this.arr=value;
       if(this.arr.length!=0){
         this.empty=true;
       }
     }
   })

  }


 /* confirm2($event: MouseEvent,id:any,index:any) {

    this.confirmationService.confirm({
      target: $event.target as EventTarget,
      message: 'Do you want to delete this project?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass:"p-button-danger p-button-text",
      rejectButtonStyleClass:"p-button-text p-button-text",
      acceptIcon:"none",
      rejectIcon:"none",

      accept: () => {

        this.srv.deleteByid(id).subscribe({

          next:value =>{
            this.arr.splice(index, 1);
            this.toast.info({detail:"INFO",summary:'the project is deleted',sticky:true});

          }
        });

      },
      reject: () => {
        this.toast.error({detail:"ERROR",summary:'Deletion Canceled',sticky:true});
      }
    });
  }*/

}
