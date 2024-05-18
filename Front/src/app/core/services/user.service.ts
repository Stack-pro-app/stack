import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../enviromnents/enviroment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  url: string = environment.API_MESAAGING_URL+'/api/User';
  constructor(private http: HttpClient) {}

  getWorkSpaces(userId: string): Observable<any> {
    const RequestUrl = `${this.url}/myworkspaces/${userId}`;
    return this.http.get(RequestUrl);
  }

  addUserToWorkspace(data: any): any {
    //TODO Add user to Workspace

    return null;
  }
  getUersFromWorkSpace(id: any): Observable<any> {
    const RequestUrl = `${this.url}/Workspace/${id}`;

    return this.http.get(RequestUrl);
  }
  addUserToWorkSpace(userId: any, id: any): Observable<any> {
    const RequestUrl = `${this.url}/Workspace`;

    let data = {
      workspaceId: id,
      usersId: [userId],
    };
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post<any>(RequestUrl, data, { headers });
  }

  FindUserByEmail(email: string): Observable<any> {
    console.log(email);

    const RequestUrl = `${this.url}/email/${email}`;

    return this.http.get(RequestUrl);
  }

  deleteUserFromWorkSpace(userId:any,id:any):Observable<any>{
    const RequestUrl = `${this.url}/${userId}/Workspace/${id}`;
    return this.http.delete(RequestUrl);
  }

  getUserById(id:any):Observable<any>{
    const RequestUrl = `${this.url}/byId/${id}`;
    return this.http.get(RequestUrl);
  }

  updateProfilePic(profilePic:FormData):Observable<any>{
    console.log(profilePic);
    const RequestUrl = `${this.url}/Picture`;
    return this.http.put(RequestUrl,profilePic);
  }

  getChannelUsers(channelId:any):Observable<any>{
    const RequestUrl = `${this.url}/channel/${channelId}`;
    return this.http.get(RequestUrl);
  }
}
