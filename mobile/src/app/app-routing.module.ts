import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  { path: 'auth/:state', loadChildren: './public/auth/auth.module#AuthPageModule' },
  { path: 'auth', loadChildren: './public/auth/auth.module#AuthPageModule' },
  { path: '**', redirectTo: 'auth', pathMatch: 'full'},
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
