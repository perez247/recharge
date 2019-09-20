import { NgModule } from '@angular/core';
import { PrivateSharedModule } from './private.shared.module';
import { PrivateRoutingModule } from './private-routing.module';

@NgModule({
  declarations: [],
  imports: [
    PrivateSharedModule,
    PrivateRoutingModule
  ]
})
export class PrivateModule { }
