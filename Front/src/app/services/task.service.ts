import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ProjectInter} from "../interfaces/project-inter";
import {TaskInter} from "../interfaces/task-inter";
import {TaskInter1} from "../interfaces/task-inter1";
import {GantInter} from "../interfaces/Gant";

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  URL = "http://localhost:8080"
  constructor(private htpp : HttpClient) { }
  createTask(T : TaskInter){

    return  this.htpp.post<TaskInter>(this.URL.concat("/task"),T);
  }
  getAllTasks(){
    return this.htpp.get<TaskInter1>(this.URL.concat("/task"));
  }
  getAllTasks00(id:any){
    return this.htpp.get<TaskInter1>(this.URL.concat("/taskAdmin/").concat(id));
  }
  deleteTask(no:any){
    return this.htpp.delete(this.URL.concat("/task/").concat(no));
  }
  getByNo(no: any){
    return this.htpp.get<TaskInter1>(this.URL.concat("/task/").concat(no));

  }
  getGant(){

    return this.htpp.get<GantInter[]>(this.URL.concat("/task/Gantt1"));
  }
  getGant0(id:any){
    return this.htpp.get<GantInter[]>(this.URL.concat("/task/Gantt/").concat(id));
  }

  updateProgrss(no:any,progress:any){
    return this.htpp.get(this.URL.concat("/task/").concat(no).concat("/").concat(progress));
  }
}
