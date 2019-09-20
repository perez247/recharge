import { Injectable, Inject } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Storage } from '@ionic/storage';
import { AppToken } from '../../app-domain/app-token';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  tokenHelper = new JwtHelperService();

  constructor(@Inject(Storage) private storage: Storage) {
   }

  save(token = null) {
    if (token) {
      // console.log(token);
      this.storage.set('token', token);

      // this.setToken();
    }
  }

  // setToken() {
  //   UserActions.currentData = this.tokenAsObject();
  // }

  async tokenAsObject(): Promise<AppToken | null> {
    // console.log(this.storage);
    // return {};
    const token = await this.storage.get('token');

    // console.log(token);

    if (!token) { return null; }

    // console.log(token);

    return token ? {
        asString: token,
        isExpired: this.tokenHelper.isTokenExpired(token),
        ...this.decode(token)
      } : null;
  }

  decode(token: string) {
    return this.tokenHelper.decodeToken(token);
  }

  clear() {
    this.storage.clear().then()
    .catch(() => console.log('failed to clear storage'));
  }


}
