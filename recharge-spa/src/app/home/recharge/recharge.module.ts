import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from './../../shared/shared.module';
import { RechargePage } from './recharge.page';

const routes: Routes = [
  {
    path: '',
    component: RechargePage
  }
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  declarations: [RechargePage]
})
export class RechargePageModule {}
