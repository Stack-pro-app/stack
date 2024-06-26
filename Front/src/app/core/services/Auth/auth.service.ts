import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { LoginResponseDto } from '../../Models/login-response-dto';
import { StoreService } from '../store/store.service';
import { ChatUser } from '../../Models/chat-user';
import { environment } from '../../../../enviromnents/enviroment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  url: string = environment.API_AUTH_URL+'/api/auth/';
  urlU: string = environment.API_MESAAGING_URL+'/api/User';
  constructor(private http: HttpClient, private store: StoreService) {}

  login(userData: any): Observable<any> {
    const loginUrl = `${this.url}login`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http
      .post<any>(loginUrl, userData, { headers })
      .pipe(tap((response) => {}));
  }
  logout() {
    localStorage.clear();
  }
  register(userData: any): Observable<any> {
    const registerUrl = `${this.url}register`;
    return this.http.post(registerUrl, userData);
  }
  getChatUser(): Observable<any> {
    const userId = this.store.getUser().sub;
    const UserUrl = `${this.urlU}/${userId}`;
    return this.http.get(UserUrl);
  }
}
