import { RegisterValidation } from '../../../shared/common/custom-validation/register-validation';
import { AuthService } from '../../../shared/_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.page.html',
  styleUrls: ['./auth.page.scss'],
})
export class AuthPage {
  signIn = true;

  constructor() {
   }

   ionViewWillEnter() {
    this.signIn = true;
  }

   toggleAuth() {
    this.signIn = !this.signIn;
   }
}
