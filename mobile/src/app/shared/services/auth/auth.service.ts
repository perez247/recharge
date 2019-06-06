import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api = `${environment.api}auth`;

  constructor(private http: HttpClient) { }

  signUp(data: any) {
    return this.http.post(`${this.api}/signup`, data);
  }

  unique(value: string) {
    return this.http.get(`${this.api}/unique?value=${value}`);
  }
}
