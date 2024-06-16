import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatStepper} from "@angular/material/stepper";
import {NgToastService} from "ng-angular-popup";
import {ProjectInter} from "../../interfaces/project-inter";
import {ProjectService} from "../../services/project.service";
import {ActivatedRoute, Route, Router} from "@angular/router";


@Component({
  selector: 'app-post-pro',
  templateUrl: './post-pro.component.html',
  styleUrl: './post-pro.component.scss'
})
export class PostProComponent implements OnInit{
  @ViewChild('stepper') stepper!: MatStepper;
  f1: FormGroup;
  f2: FormGroup;
  saved: boolean=false;
  act1:ActivatedRoute  ;
  idAdmin :any;
  works : any ;


  constructor(private router: Router,private act0:ActivatedRoute,private fb: FormBuilder, private toast: NgToastService,private srv:ProjectService) {
    this.act1=act0;

    this.f1 = fb.group({
      projectName: ['', Validators.required],
      projectDescr: ['', Validators.required],
      start : ['',Validators.required],
      end : ['',Validators.required],
      workId : ['',Validators.required]
    });
    this.f2=fb.group(
      {
        ClientName:[''],
        Budget : [''],

      }
    );

  }

  ngOnInit(): void {
    console.log(this.act1.paramMap.subscribe({
        next :(value)=>{console.log(value.get('id'));this.idAdmin=value?.get('id');},
      }
    ));
    this.srv.findWorks(this.idAdmin).subscribe({
      next : value => this.works = value ,
      error :value=> console.log("EROOR")
    })

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
      start: this.transformDate(this.f1.get('start')?.value),
      workId : this.f1.get('workId')?.value
    }


    this.srv.createExam(pro).subscribe(
      {
        next : value =>{this.toast.success({detail:"SUCCESS",summary:'Project CREATED WITH SUCCESS'});this.saved=true;this.stepper.next()} ,
        error :value=> {this.toast.error({detail:"ERROR",summary:'NOT created ',sticky:true});this.saved=false;this.stepper.next()}
      }
    );


  }
  goToProject() {

    const idAdm  = this.idAdmin ;
    console.log(idAdm);
    // Navigate to the current URL followed by '/post'


    this.router.navigateByUrl(`project/${idAdm}`)
  }

}

