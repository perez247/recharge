import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  private apiUrl = environment.baseUrlApi + 'home';

  constructor(
    private http: HttpClient,
    ) { }

  getPoint() {
    return this.http.get(`${this.apiUrl}`);
  }

  getUserTransactions() {
    return this.http.get(`${this.apiUrl}/mytransactions`);
  }
}
