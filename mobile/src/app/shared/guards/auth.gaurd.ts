import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from '../state.management/store';
import * as _ from 'lodash';
import { AppRoutes } from '../routes/app.routes';

@Injectable()

export class AuthGuard implements CanActivate {

    appRoutes = AppRoutes.generateRoutes();

    constructor(
        private router: Router,
        private redux: NgRedux<IAppState>
        ) {}

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
        const token = await this.redux.getState().user.token;

        if (_.isEmpty(token) || token.isExpired) {

            this.router.navigate(['/' + this.appRoutes.public.signIn.path ], { queryParams: { returnUrl: state.url } });

            return false;
        } else if (!_.isEmpty(token) && !token.isExpired && token.isConfirmed.toLowerCase() === 'false') {

            this.router.navigate(['/' + this.appRoutes.public.confirmPhone.path ]);
            console.log(token);
            return false;
        }

        return true;
    }
}

