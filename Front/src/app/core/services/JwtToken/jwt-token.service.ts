import { Injectable } from '@angular/core';
import * as jwt_decode from 'jwt-decode';
@Injectable({
  providedIn: 'root',
})
export class JwtTokenService {
  constructor() {}

  DecodeToken(token:string):string{
      return jwt_decode.jwtDecode(token);
  }
}
