import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  url: string = 'http://localhost:8193/api/User';
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

}
