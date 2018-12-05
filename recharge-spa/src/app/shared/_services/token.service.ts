import { AuthService } from './auth.service';
import { AppUser } from './../model/app-user';
import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { JwtHelperService } from '@auth0/angular-jwt';
import { from, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  tokenHelper = new JwtHelperService();

  constructor(private storage: Storage) { }

  save(token = null, user = null) {
    if (token) {
      this.storage.set('token', token);
    }

    if (user) {
      this.storage.set('user', JSON.stringify(user));
    }
  }

  getUserAsObject() {
    return from<AppUser>(this.storage.get('user').then(x => {
      if (!x) {return null; } else {return JSON.parse(x); }
    }));
  }

  getToken() {
    return from<AppUser>(this.storage.get('token').then(x => {
      if (!x) {return null; } else {return x; }
    }));
  }

  clear() {
    this.storage.remove('token');
    this.storage.remove('user');
  }

  getTokenAsObject() {
    return from(this.storage.get('token').then(x => {
      if (!x) {return null; } else {
        return this.tokenHelper.decodeToken(x);
      }
    }));
  }
}
