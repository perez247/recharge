import { NgRedux, NgReduxModule } from '@angular-redux/store';
import { Injector, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouteReuseStrategy } from '@angular/router';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { IonicStorageModule } from '@ionic/storage';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule} from './shared/shared.module';
import { IAppState, INITIAL_STATE, rootReducer } from './shared/state.management/store';
import { TokenService } from './shared/services/token/token.service';
import { AppInjector } from './shared/state.management/user-state/user-store';

@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    IonicModule.forRoot(),
    IonicStorageModule.forRoot(),
    AppRoutingModule,
    NgReduxModule,
    SharedModule,
  ],
  providers: [
    StatusBar,
    SplashScreen,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy }
  ],
  bootstrap: [AppComponent]
  // 
})


export class AppModule {
  constructor(private injector: Injector, ngRedux: NgRedux<IAppState>) {
    AppInjector.tokenService = injector.get<TokenService>(TokenService);
    ngRedux.configureStore(rootReducer, INITIAL_STATE);
  }
}
