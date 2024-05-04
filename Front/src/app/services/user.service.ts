import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ProjectInter} from "../interfaces/project-inter";
import {UserInter} from "../interfaces/user-inter";
import {ActivityInter} from "../interfaces/Activity-inter";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  URL = "http://localhost:8080"
  constructor(private htpp : HttpClient) { }
  findAll(id:any){
    return this.htpp.get<UserInter>(this.URL.concat("/user/").concat(id));
  }
  getWithTsk(){
    return this.htpp.get<UserInter[]>(this.URL.concat("/userAndTasks"));
  }
  IdByName(name:any){

    return this.htpp.get(this.URL.concat("/user/").concat("IdByName/").concat(name));
  }
  getTasks(id:any){
    return this.htpp.get<UserInter>(this.URL.concat("/userTasks/").concat(id));
  }
getActivity(userId : any, taskId : any){
    return this.htpp.get<ActivityInter[]>(this.URL.concat("/Activity/").concat(userId).concat("/").concat(taskId));
}
sameWork(pro_id:any,user_id:any){
    return this.htpp.get(this.URL.concat("/user/sameWork/").concat(pro_id).concat("/").concat(user_id));
}

}
