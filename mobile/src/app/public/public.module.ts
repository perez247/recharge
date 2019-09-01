import { NgModule } from '@angular/core';
import { PublicComponent } from './public.component';
import { PublicSharedModule } from './public.shared.module';
import { PublicRoutingModule } from './public-routing.module';

@NgModule({
    declarations: [PublicComponent],
    imports: [
      PublicSharedModule,
      PublicRoutingModule,
    ]
  })
  export class PublicModule { }
