import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './pages/home/home.component';

@NgModule({
  declarations: [
    HomeComponent,
  ],
  imports: [
    SharedModule
  ],
  exports: [
    SharedModule,
    HomeComponent
  ]
})
export class PrivateSharedModule { }
