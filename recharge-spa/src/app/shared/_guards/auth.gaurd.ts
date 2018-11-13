import { CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private auth: AuthService, private router: Router) { }

  canActivate(route, state: RouterStateSnapshot) {

    return this.auth.user().pipe(map(user => {
        if (user) {
            return true;
        }

        this.auth.logout();
        this.router.navigate(['auth'], {queryParams: {
            returnUrl: state.url
        }});
        return false;
    }));
  }

}
