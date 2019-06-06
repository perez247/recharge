import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgMaterialModule } from './ng-material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { AuthService } from './services/auth/auth.service';
import { RequestValidations } from './validations/request-validations';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    NgMaterialModule,
    RouterModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers : [
    RequestValidations,
    AuthService,
  ],
  exports: [
    CommonModule,
    FormsModule,
    IonicModule,
    NgMaterialModule,
    RouterModule,
    ReactiveFormsModule,
    HttpClientModule,
  ]
})
export class SharedModule { }
