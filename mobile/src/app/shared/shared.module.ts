import { NgModule, Injector } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgMaterialModule } from './ng-material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { AuthService } from './services/auth/auth.service';
import { RequestValidations } from './validations/request-validations';
import { HttpClientModule } from '@angular/common/http';
import { ValidatorErrorMessageComponent } from './components/validator-error-message/validator-error-message.component';
import { AppErrorService } from './services/errors/app-error.service';
import { TokenService } from './services/token/token.service';
import { AuthGuard } from './guards/auth.gaurd';
import { LogoHeadingComponent } from '../public/components/logo-heading/logo-heading.component';

@NgModule({
  declarations: [
    ValidatorErrorMessageComponent,
    LogoHeadingComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    NgMaterialModule,
    RouterModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers : [
    RequestValidations,
    AuthService,
    AppErrorService,
    TokenService,
    AuthGuard
  ],
  exports: [
    CommonModule,
    FormsModule,
    IonicModule,
    NgMaterialModule,
    RouterModule,
    ReactiveFormsModule,
    HttpClientModule,
    ValidatorErrorMessageComponent,
    LogoHeadingComponent
  ]
})
export class SharedModule {}
