import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from './../../shared/shared.module';
import { DownLinksPage } from './down-links.page';

const routes: Routes = [
  {
    path: '',
    component: DownLinksPage
  }
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  declarations: [DownLinksPage]
})
export class DownLinksPageModule {}
