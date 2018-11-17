import { AuthService } from './auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { map, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  private apiUrl = environment.baseUrlApi + 'home';

  constructor(
    private http: HttpClient,
    private authService: AuthService
    ) { }

  get() {
    return this.authService.user().pipe(switchMap(token => {
      const id: string = token.nameid;
      return this.http.get(`${this.apiUrl}/${id}`);
    }));
  }
}
