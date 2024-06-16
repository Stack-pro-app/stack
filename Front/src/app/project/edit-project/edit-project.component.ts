import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {ProjectService} from "../../services/project.service";
import {ProjectInter} from "../../interfaces/project-inter";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NgToastService} from "ng-angular-popup";
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrl: './edit-project.component.scss'
})
export class EditProjectComponent implements OnInit{
  act1:ActivatedRoute  ;

  f1: FormGroup;
  id:any ;
  // @ts-ignore
  pro : ProjectInter ;
  changed : boolean =false;
  constructor(private act0:ActivatedRoute,private toast: NgToastService,private srv:ProjectService,private fb: FormBuilder,private router:Router) {
    this.act1=act0;
    this.f1=fb.group({
      projectName: ['', Validators.required],
      projectDescr: ['', Validators.required],
      start : ['',Validators.required],
      end : ['',Validators.required],
      ClientName:[''],
      Budget : [''],
    });
  }

  ngOnInit(): void {
    console.log(this.act1.paramMap.subscribe({
        next :(value)=>{console.log(value);this.id=value?.get('id');console.log(this.id)},
      }
    ));
    this.srv.findById(this.id).subscribe({
      next:value => {this.pro=value;this.populateForm();this.changed=false}
    });

    this.f1.valueChanges.subscribe({
      next:value => this.changed=true
    });

  }

  save() {

    this.pro = {
      id:this.pro.id,
      budget: this.f1.get('Budget')?.value,
      clientName: this.f1.get('ClientName')?.value,
      end: this.transformDate(this.f1.get('end')?.value),
      projectDescrp: this.f1.get('projectDescr')?.value,
      projectName: this.f1.get('projectName')?.value,
      start: this.transformDate(this.f1.get('start')?.value),
      workId : this.pro.workId
    }
    //console.log(this.pro);

    this.srv.createExam(this.pro).subscribe(
      {
        next:value => {
          this.toast.success({detail:"SUCCESS",summary:'Project Updated WITH SUCCESS'});
          this.changed=false ;

        }
      }
    );

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
  redirectToPst(): void {
    this.router.navigate(['/project'])
  }
  populateForm(): void {
    if (this.pro) {
      this.f1.patchValue({
        projectName: this.pro.projectName,
        projectDescr: this.pro.projectDescrp,
        start: this.pro.start ? new Date(this.pro.start) : null,
        end: this.pro.end ? new Date(this.pro.end) : null,
        ClientName: this.pro.clientName,
        Budget: this.pro.budget,
      });
    }}
}
