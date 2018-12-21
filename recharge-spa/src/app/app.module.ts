import { AppErrorHandler } from './shared/common/app.error.handler';
import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { IonicStorageModule } from '@ionic/storage';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt';
import { Storage } from '@ionic/storage';
import { FormattedAmountDirective } from './shared/directives/formatted-amount.directive';

// const storage = new Storage({
//   name: 'myapp',
//   driverOrder: ['sqlite', 'indexeddb', 'websql'],
// });

// export function jwtOptionsFactory(storage) {
//   return {
//     tokenGetter: () => {
//       return storage.get('token');
//     },
//     whitelistedDomains: ['localhost:5000'],
//     blacklistedRoutes: ['localhost:5000/api/auth', 'localhost:5000/api/confirm-phone']
//   };
// }



@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    IonicStorageModule.forRoot(),
    AppRoutingModule,
    SharedModule
  ],
  providers: [
    StatusBar,
    SplashScreen,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    {provide: ErrorHandler, useClass: AppErrorHandler},
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
