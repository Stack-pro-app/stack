import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  url: string = 'https://localhost:8193/api/User';
  constructor(private http: HttpClient) {}

  getWorkSpaces(userId: string): Observable<any> {
    const RequestUrl = `${this.url}/myworkspaces/${userId}`;
    return this.http.get(RequestUrl);
  }

  addUserToWorkspace(data: any): any{
    //TODO Add user to Workspace
    return null;
  }
}
