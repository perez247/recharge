import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ConfirmPhoneComponent } from './pages/confirm-phone/confirm-phone.component';
import { HomeComponent } from './pages/home/home.component';

@NgModule({
  declarations: [
    ConfirmPhoneComponent,
    HomeComponent,
  ],
  imports: [
    SharedModule
  ],
  exports: [
    SharedModule,
    ConfirmPhoneComponent,
    HomeComponent
  ]
})
export class PrivateSharedModule { }
