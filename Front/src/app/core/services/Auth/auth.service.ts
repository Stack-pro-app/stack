import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { LoginResponseDto } from '../../Models/login-response-dto';
import { StoreService } from '../store/store.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  url: string = 'https://localhost:7004/api/auth/';
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
}
