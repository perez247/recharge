import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private apiUrl = environment.baseUrlApi + 'transaction';

  constructor(
    private http: HttpClient,
    ) { }

  getUser() {
    return this.http.get(this.apiUrl);
  }
}
