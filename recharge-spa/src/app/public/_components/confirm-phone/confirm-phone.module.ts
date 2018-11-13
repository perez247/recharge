import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from './../../../shared/shared.module';
import { ConfirmPhonePage } from './confirm-phone.page';

const routes: Routes = [
  {
    path: '',
    component: ConfirmPhonePage
  }
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes),
  ],
  providers: [
  ],
  declarations: []
})
export class ConfirmPhonePageModule {}
