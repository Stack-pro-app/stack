import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private httpClient:HttpClient) { }

  GetUserWorkspace(userId:number):any {
    return this.httpClient.get("https://localhost:7005/api/User/myworkspaces/"+userId)
  }
}
