import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './_services/auth.service';
import { TokenService } from './_services/token.service';
import { RegisterValidation } from './common/register-validation';
import { AuthPage } from '../public/_components/auth/auth.page';
import { RegisterPage } from '../public/_components/register/register.page';
import { LoginPage } from '../public/_components/login/login.page';
import { ConfirmPhonePage } from '../public/_components/confirm-phone/confirm-phone.page';

@NgModule({
  imports: [
    FormsModule,
    IonicModule,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    AuthService,
    TokenService,
    RegisterValidation
  ],
  declarations: [
    AuthPage,
    RegisterPage,
    LoginPage,
    ConfirmPhonePage
  ],
  exports: [
    FormsModule,
    IonicModule,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    AuthPage,
    RegisterPage,
    LoginPage,
    ConfirmPhonePage,
  ]
})
export class SharedModule { }
