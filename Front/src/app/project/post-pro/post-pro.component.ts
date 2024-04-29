import {Component, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatStepper} from "@angular/material/stepper";
import {NgToastService} from "ng-angular-popup";
import {ProjectInter} from "../../interfaces/project-inter";
import {ProjectService} from "../../services/project.service";


@Component({
  selector: 'app-post-pro',
  templateUrl: './post-pro.component.html',
  styleUrl: './post-pro.component.scss'
})
export class PostProComponent {
  @ViewChild('stepper') stepper!: MatStepper;
  f1: FormGroup;
  f2: FormGroup;
  saved: boolean=false;


  constructor(private fb: FormBuilder, private toast: NgToastService,private srv:ProjectService) {

    this.f1 = fb.group({
      projectName: ['', Validators.required],
      projectDescr: ['', Validators.required],
      start : ['',Validators.required],
      end : ['',Validators.required]
    });
    this.f2=fb.group(
      {
        ClientName:[''],
        Budget : [''],

      }
    );

  }

  checkName() {
   this.srv.exists(this.f1.get('projectName')?.value).subscribe(
     {
       next:value => {
         if(value==true){
           this.toast.error({detail:"ERROR",summary:'Project Name alredy exits',sticky:true});
         }
         else{
           this.stepper.next();
         }
       }
     }
   )
  }
  transformDate(date : any) {
    const startDate = new Date(date);

    const formattedStartDate = startDate.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });

    return formattedStartDate ;


  }


  save() {
    let pro: ProjectInter = {
      budget: this.f2.get('Budget')?.value,
      clientName: this.f2.get('ClientName')?.value,
      end: this.transformDate(this.f1.get('end')?.value),
      projectDescrp: this.f1.get('projectDescr')?.value,
      projectName: this.f1.get('projectName')?.value,
      start: this.transformDate(this.f1.get('start')?.value)
    }

    this.srv.createExam(pro).subscribe(
      {
        next : value =>{this.toast.success({detail:"SUCCESS",summary:'EXAM CREATED WITH SUCCESS'});this.saved=true;this.stepper.next()} ,
        error :value=> {this.toast.error({detail:"ERROR",summary:'NOT created ',sticky:true});this.saved=false;this.stepper.next()}
      }
    );


  }

}

