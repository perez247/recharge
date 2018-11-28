import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WithdrawService {
  private apiUrl = environment.baseUrlApi + 'withdraw';

  constructor(private http: HttpClient) { }

  withdraw(data: any) {
    return this.http.post(`${this.apiUrl}`, data);
  }
}
