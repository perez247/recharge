import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { TokenService } from '../token/token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api = `${environment.api}auth`;

  constructor(private http: HttpClient, private tokenService: TokenService) { }

  getTokenService() {
    return this.tokenService;
  }

  signUp(data: any) {
    return this.http.post(`${this.api}/signup`, data);
  }

  unique(value: string) {
    return this.http.get(`${this.api}/unique?value=${value}`);
  }
}
