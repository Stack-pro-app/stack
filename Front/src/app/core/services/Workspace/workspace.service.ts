import { Injectable } from '@angular/core';
import { Workspace } from '../../Models/workspace';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../../enviromnents/enviroment';

@Injectable({
  providedIn: 'root',
})
export class WorkspaceService {
  url: string = environment.API_MESAAGING_URL + '/api/Workspace';

  constructor(private http: HttpClient) {}

  Create(obj: any): Observable<any> {
    console.log(obj);
    const WorkespaceDto = {
      adminId: obj.adminId,
      name: obj.name,
    };
    const CreateUrl = `${this.url}`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    console.log(WorkespaceDto);
    return this.http
      .post<any>(CreateUrl, WorkespaceDto, { headers })
      .pipe(
        tap((response) => console.log('CreateWorkspace Response:', response))
      );
  }
  /*getWorkspace(id: any, userId: any): Observable<any> {
    const RequestUrl = `${this.url}/${id}/user/${userId}`;
    return this.http.get(RequestUrl);
  }*/
  getWorkspace(id: any, userId: any): Observable<any> {
    const RequestUrl = `${this.url}/${id}`;
    const params = new HttpParams().set('userId', userId);
    return this.http.get(RequestUrl, { params });
  }
  Delete(id: any): Observable<any> {
    const RequestUrl = `${this.url}/${id}/`;
    return this.http.delete(RequestUrl);
  }
  Update(id: any, diffname: string) {
    const RequestUrl = `${this.url}/${id}`;
    const data = {
      name: diffname,
    };
    const headers = new HttpHeaders({
      accept: 'text/plain',
      'Content-Type': 'application/json',
    });
    return this.http.put(RequestUrl, diffname, { headers });
  }
  getUserSInvitions(id:any):Observable<any>{
    return this.http.get(`${environment.API_MESAAGING_URL}/api/Invitation/${id}`);
  }
  onAcceptInvitation(token:any):Observable<any>{
    return this.http.put(`${environment.API_MESAAGING_URL}/api/Invitation/accept/${token[0].token}`,{});
  }
  onDeclineInvitation(token:any):Observable<any>{
    console.log(token[0].token);

    return this.http.put(`${environment.API_MESAAGING_URL}/api/Invitation/decline/${token[0].token}`,{});
  }
  onInviteUser(userId:any,workspaceId:any):Observable<any>{
    const requestBody =
      {
        "workspaceId": workspaceId,
        "userId": userId
      };

    return this.http.post(`${environment.API_MESAAGING_URL}/api/Invitation`,requestBody);
  }
}
