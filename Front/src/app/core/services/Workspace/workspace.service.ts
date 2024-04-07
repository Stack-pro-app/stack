import { Injectable } from '@angular/core';
import { Workspace } from '../../Models/workspace';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class WorkspaceService {
  url: string = 'http://localhost:5149/api/Workspace';

  constructor(private http: HttpClient) {}

  Create(obj: Workspace): Observable<any> {
    console.log(obj);
    const WorkespaceDto = {
      userId: 1,
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
  getWorkspace(id: any, userId: any): Observable<any> {
    const RequestUrl = `${this.url}/${id}/user/${userId}`;
    return this.http.get(RequestUrl);
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
}
