import { AppToken } from './../model/app-token';
import { TokenService } from './token.service';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppUser } from '../model/app-user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.baseUrlApi + 'auth';
  token: AppToken;
  authUser: AppUser;

  constructor(private http: HttpClient, private tokenService: TokenService) {
    // this.tokenService.refresher.subscribe(user => this.authUser = user);
   }

  setUser() {
    // console.log('token 2');
    // this.tokenService.getUserAsObject().subscribe(x => this.authUser = x);
    return this.tokenService.getUserAsObject();
  }

  register(user: any) {
    return this.http.post<{user: AppUser, token: string}>(`${this.apiUrl}/register`, user);
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
    return this.http.get<{token: string, user: AppUser}>(`${this.apiUrl}/resendcode/${userId}`);
  }
}
