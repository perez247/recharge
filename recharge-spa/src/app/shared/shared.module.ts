import { MobileValidation } from './common/custom-validation/mobile-validation';
import { JwtInterceptorProvider } from './common/jwt.interceptor';
import { AuthGuardService } from './_guards/auth.gaurd';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './_services/auth.service';
import { TokenService } from './_services/token.service';
import { RegisterValidation } from './common/custom-validation/register-validation';
import { AuthPage } from '../public/_components/auth/auth.page';
import { RegisterPage } from '../public/_components/register/register.page';
import { LoginPage } from '../public/_components/login/login.page';
import { ConfirmPhonePage } from '../public/_components/confirm-phone/confirm-phone.page';
import { HomeService } from './_services/home.service';
import { ErrorInterceptorProvider } from './common/error.interceptor';
import { PaymentsPage } from '../home/payments/payments.page';
import { RechargeService } from './_services/recharge.service';
import { PaymentValidation } from './common/custom-validation/payment-validation';

@NgModule({
  imports: [
    FormsModule,
    IonicModule,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [
    AuthService,
    TokenService,
    RegisterValidation,
    HomeService,
    AuthGuardService,
    ErrorInterceptorProvider,
    JwtInterceptorProvider,
    MobileValidation,
    PaymentValidation,
    RechargeService
  ],
  declarations: [
    AuthPage,
    RegisterPage,
    LoginPage,
    ConfirmPhonePage,
    PaymentsPage,
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
    PaymentsPage,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
