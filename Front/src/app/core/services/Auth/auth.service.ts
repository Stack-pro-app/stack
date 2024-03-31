import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { LoginResponseDto } from '../../Models/login-response-dto';
import { StoreService } from '../store/store.service';
import { ChatUser } from '../../Models/chat-user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  url: string = 'https://localhost:7004/api/auth/';
  urlU: string = 'https://localhost:8193/api/User';
  constructor(private http: HttpClient, private store: StoreService) {}

  login(userData: any): Observable<any> {
    console.log(userData);
    const loginUrl = `${this.url}login`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http
      .post<any>(loginUrl, userData, { headers })
      .pipe(tap((response) => console.log('Login Response:', response)));
  }
  logout() {
    console.log(this.store.isLogged());
    localStorage.removeItem('token');
    console.log(this.store.isLogged());
  }
  register(userData: any): Observable<any> {
    console.log(userData);
    const registerUrl = `${this.url}register`;
    return this.http.post(registerUrl, userData);
  }
  getChatUser(): Observable<any> {
    const userId = this.store.getUser().sub;
    console.log(userId);
    const UserUrl = `${this.urlU}/${userId}`;
    return this.http.get(UserUrl);
  }
}
