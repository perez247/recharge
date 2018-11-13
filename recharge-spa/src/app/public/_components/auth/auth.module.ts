import { RegisterValidation } from './../../../shared/common/register-validation';
import { AuthService } from '../../../shared/_services/auth.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { AuthPage } from './auth.page';
import { RegisterPage } from '../register/register.page';
import { HttpClientModule } from '@angular/common/http';
import { LoginPage } from '../login/login.page';
import { TokenService } from '../../../shared/_services/token.service';
import { SharedModule } from '../../../shared/shared.module';

const routes: Routes = [
  {
    path: '',
    component: AuthPage
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    SharedModule
  ],
  providers: [
  ],
  declarations: [
  ]
})
export class AuthPageModule {}
