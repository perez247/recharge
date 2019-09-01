import { AppRoutes } from '../shared/routes/app.routes';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { NgModule } from '@angular/core';
import { ConfirmPhoneComponent } from './pages/confirm-phone/confirm-phone.component';



const appRoutes = AppRoutes.generateRoutes();

const routes: Routes = [
    {
        path: '',
        redirectTo: '',
        pathMatch: 'full',
    },
    { path: appRoutes.private.confirmPhone.name, component: ConfirmPhoneComponent },
    { path: appRoutes.private.home.name, component: HomeComponent },
    { path: '**', redirectTo: appRoutes.private.home.name, pathMatch: 'full'},

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class PrivateRoutingModule { }


