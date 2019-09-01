import { Component } from '@angular/core';

import { Platform } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from './shared/state.management/store';
import { UserActionConstant } from './shared/state.management/user-state/user-action-constant';
import { TokenService } from './shared/services/token/token.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html'
})
export class AppComponent {
  constructor(
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private redux: NgRedux<IAppState>,
    private tokenService: TokenService
  ) {
    this.initializeApp();
  }

  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();
      // this.tokenService.initializeStateManagement();
      this.redux.dispatch({type: UserActionConstant.SET_AUTH_USER});
    });
  }
}
