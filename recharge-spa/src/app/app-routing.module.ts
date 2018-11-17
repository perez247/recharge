import { AuthGuardService } from './shared/_guards/auth.gaurd';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuardService],
    children: [
      {
        path: 'home',
        loadChildren: './home/home/home.module#HomePageModule'
      },
      {
        path: 'mobile',
        loadChildren: './home/recharge/mobile/mobile.module#MobilePageModule'
      },
    ]
  },
  {
    path: 'auth',
    loadChildren: './public/_components/auth/auth.module#AuthPageModule'
  },
  {
    path: 'confirm-phone',
    loadChildren: './public/_components/confirm-phone/confirm-phone.module#ConfirmPhonePageModule'
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
