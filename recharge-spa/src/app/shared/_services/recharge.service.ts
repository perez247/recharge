import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';

import { environment } from '../../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RechargeService {
  private apiUrl = environment.baseUrlApi + 'recharge';
  typeDataObserver = new BehaviorSubject<FormGroup>(new FormGroup({}));
  typeData = this.typeDataObserver.asObservable();

  constructor(
    private http: HttpClient,
    private authService: AuthService
    ) { }

  recharge(data: any, type: string) {
    return this.http.post(`${this.apiUrl}/${type}`, data);
  }

  getUser() {
    return this.http.get(`${this.apiUrl}/with-card`);
  }

  refreshTypeData(data: FormGroup) {
    this.typeDataObserver.next(data);
  }
}
