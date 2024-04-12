import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ProjectInter} from "../interfaces/project-inter";

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  URL = "http://localhost:8080"
  constructor(private htpp : HttpClient) { }

  createExam(T : ProjectInter){

    return  this.htpp.post<ProjectInter>(this.URL.concat("/project"),T);
  }
  exists(name:string){
    return  this.htpp.get(this.URL.concat("/project").concat("/exists/").concat(name));
}
  findAll(){
    return this.htpp.get<ProjectInter>(this.URL.concat("/project"))
  }
  findById(id : string){
    return this.htpp.get<ProjectInter>(this.URL.concat("/project/").concat(id));
  }
  deleteByid(id:string){
    return this.htpp.delete(this.URL.concat("/project/").concat(id));
  }
  IdByName(name:any){

    return this.htpp.get(this.URL.concat("/project/").concat("IdByName/").concat(name));
  }
}
