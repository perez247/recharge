import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { SigninComponent } from './pages/signin/signin.component';
import { SignupComponent } from './pages/signup/signup.component';
import { AppRoutes } from '../shared/routes/app.routes';


const appRoutes = AppRoutes.generateRoutes();

const routes: Routes = [
    {
        path: '',
        redirectTo: appRoutes.public.signIn.name,
        pathMatch: 'full',
    },
    { path: appRoutes.public.signIn.name, component: SigninComponent },
    { path: appRoutes.public.signUp.name, component: SignupComponent },
    { path: '**', redirectTo: appRoutes.public.signIn.name, pathMatch: 'full'},
];


@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class PublicRoutingModule { }

