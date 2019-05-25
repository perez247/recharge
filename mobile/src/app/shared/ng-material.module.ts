import { NgModule } from '@angular/core';
import { MatFormFieldModule, MatInputModule, MatSelectModule } from '@angular/material';

@NgModule({
  declarations: [],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
  ],
  exports: [
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule
  ]
})
export class NgMaterialModule { }


