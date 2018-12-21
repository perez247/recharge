import { MobilePage } from './../home/recharge/mobile/mobile.page';
import { CardDetailsPage } from './../home/settings/card-details/card-details.page';
import { BankDetailsPage } from './../home/settings/bank-details/bank-details.page';
import { PhoneDetailPage } from './../home/settings/phone-detail/phone-detail.page';
import { MyTransactionsPage } from './../home/down-links/my-transactions/my-transactions.page';
import { MyDownLinkPage } from './../home/down-links/my-down-link/my-down-link.page';
import { HomePage } from './../home/home/home.page';
import { WithdrawService } from './_services/withdraw.service';
import { BankService } from './_services/bank.service';
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
import { ToasterService } from './_services/toaster.service';
import { RouterModule } from '@angular/router';
import { OtherTransactionsPage } from '../home/down-links/other-transactions/other-transactions.page';
import { FormattedAmountDirective } from './directives/formatted-amount.directive';
import { FormattedAmountPipe } from './pipes/formatted-amount.pipe';

@NgModule({
  imports: [
    FormsModule,
    IonicModule,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule
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
    RechargeService,
    BankService,
    ToasterService,
    WithdrawService
  ],
  declarations: [
    AuthPage,
    RegisterPage,
    LoginPage,
    ConfirmPhonePage,
    PaymentsPage,
    HomePage,
    MyDownLinkPage,
    MyTransactionsPage,
    OtherTransactionsPage,
    PhoneDetailPage,
    BankDetailsPage,
    CardDetailsPage,
    MobilePage,
    FormattedAmountDirective,
    FormattedAmountPipe,
    FormattedAmountPipe
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
    ReactiveFormsModule,
    HomePage,
    MyDownLinkPage,
    MyTransactionsPage,
    OtherTransactionsPage,
    PhoneDetailPage,
    BankDetailsPage,
    CardDetailsPage,
    MobilePage,
    FormattedAmountDirective,
    FormattedAmountPipe
  ]
})
export class SharedModule { }
