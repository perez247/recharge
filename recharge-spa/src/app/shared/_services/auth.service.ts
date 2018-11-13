import { AppToken } from './../model/app-token';
import { TokenService } from './token.service';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppUser } from '../model/app-user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.baseUrlApi + 'auth';
  token: AppToken;

  constructor(private http: HttpClient, private tokenService: TokenService) {
   }

  user(): Observable<AppToken> {
    // console.log('token 2');
    return this.tokenService.getAsObject();
  }

  register(user: any) {
    return this.http.post<AppUser>(`${this.apiUrl}/register`, user);
  }

  exists(name: string) {
    return this.http.get(`${this.apiUrl}/exists/${name}`);
  }

  login(data: any) {
    return this.http.post(`${this.apiUrl}/login`, data);
  }

  logout() {
    this.tokenService.clear();
  }

  confirm(data: any) {
    return this.http.post(`${this.apiUrl}/verifyphone`, data);
  }

  resendCode(userId: string) {
    return this.http.get(`${this.apiUrl}/resendcode/${userId}`);
  }
}
