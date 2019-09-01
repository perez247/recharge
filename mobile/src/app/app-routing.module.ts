import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AppRoutes } from './shared/routes/app.routes';
import { AuthGuard } from './shared/guards/auth.gaurd';

// Get the whole routes of the app
const appRoutes = AppRoutes.generateRoutes();

const routes: Routes = [
  { path: '', redirectTo: appRoutes.public.name, pathMatch: 'full' },


  // { path: 'auth/:state', loadChildren: './public/auth/auth.module#AuthPageModule' },
  // { path: 'auth', loadChildren: './public/auth/auth.module#AuthPageModule' },

  {
    path: appRoutes.public.name,
    loadChildren: './public/public.module#PublicModule',
  },
  {
    path: appRoutes.private.name,
    loadChildren: './private/private.module#PrivateModule',
    canActivate: [AuthGuard]
  },
  { path: '**', redirectTo: appRoutes.public.name, pathMatch: 'full'},

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
