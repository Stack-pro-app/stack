import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ProjectInter} from "../interfaces/project-inter";
import {UserInter} from "../interfaces/user-inter";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  URL = "http://localhost:8080"
  constructor(private htpp : HttpClient) { }
  findAll(){
    return this.htpp.get<UserInter>(this.URL.concat("/user"));
  }
  getWithTsk(){
    return this.htpp.get<UserInter>(this.URL.concat("/userAndTasks"));
  }
  IdByName(name:any){

    return this.htpp.get(this.URL.concat("/user/").concat("IdByName/").concat(name));
  }

}
