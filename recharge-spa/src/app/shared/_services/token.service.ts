import { AppToken } from './../model/app-token';
import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { JwtHelperService } from '@auth0/angular-jwt';
import { from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  tokenHelper = new JwtHelperService();

  constructor(private storage: Storage) { }

  save(token: string) {
    this.storage.set('token', token);
  }

  getAsObject() {
    // return this.storage.get('token');
    return from(this.storage.get('token').then(tokenString => {

      if (!tokenString) {
        return null;
      }

      const tokenObject = this.tokenHelper.decodeToken(tokenString);

      if (!tokenObject) { return null; }

      return tokenObject;

    }));
    // if (!tokenString) {return of(null); }

    // const tokenObject = this.tokenHelper.decodeToken(tokenString);

    // if (!tokenObject) { return of(null); }

    // return of(tokenObject);
  }

  getAsString() {
    // const tokenString = await this.storage.get('token');
    // if (!tokenString) {return from(null); }

    // return from(tokenString);
    return from(this.storage.get('token').then(x => {
      if (!x) {return null; } else {return x; }
    }));
  }

  clear() {
    this.storage.remove('token');
  }
}
