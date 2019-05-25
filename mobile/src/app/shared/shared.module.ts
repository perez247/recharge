import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgMaterialModule } from './ng-material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    NgMaterialModule,
    RouterModule,
    ReactiveFormsModule,
  ],
  exports: [
    CommonModule,
    FormsModule,
    IonicModule,
    NgMaterialModule,
    RouterModule,
    ReactiveFormsModule,
  ]
})
export class SharedModule { }
