import { Injectable } from '@angular/core';
import { JwtTokenService } from '../JwtToken/jwt-token.service';
import { AuthService } from '../Auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class StoreService {
  constructor(private decoder: JwtTokenService) {}

  setToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLogged(): boolean {
    if (this.getToken() != null) {
      return true;
    }
    return false;
  }
  getUser(): any {
    let CurrentToken = this.getToken();
    if (CurrentToken != null) {
      const decoded = this.decoder.DecodeToken(CurrentToken);
      return decoded;
    }
  }
 
}
