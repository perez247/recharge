import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RechargeService {
  private apiUrl = environment.baseUrlApi + 'recharge';

  constructor(private http: HttpClient) { }

  recharge(data: any, type: string) {
    return this.http.post(`${this.apiUrl}/${type}`, data);
  }
}
