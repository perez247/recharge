import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RequestValidations } from 'src/app/shared/validations/request-validations';
import { MobileValidation } from 'src/app/shared/validations/mobile-validation';
import { CoreValidation } from 'src/app/shared/validations/core-validation';
import { AuthService } from 'src/app/shared/services/auth/auth.service';
import * as lodash from 'lodash';
import { ServerError, AppErrors } from 'src/app/shared/interceptors/app-error-handler';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {

  signUpForm: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService) { }

  ngOnInit() {
    this.initializeSignUpForm();
  }

  initializeSignUpForm() {
    this.signUpForm  = this.fb.group({
      countryCode : [null, Validators.required],
      phoneNumber : [null, [Validators.required, MobileValidation.validPhoneNumber('countryCode')]],
      pin : [null, [Validators.required, CoreValidation.ValidPin]],
      confirmPin : [null, [Validators.required, CoreValidation.confirmation('pin')]],
      referersCountryCode : [null],
      referersPhoneNumber : [null, MobileValidation.validPhoneNumber('referersCountryCode', false)]
    });
  }

  SignUp() {
    this.authService.signUp(this.signUpForm.value).subscribe(x => {
      console.log('created');
    },
    (error: any) => { AppErrors.setError(error.error as ServerError, this.signUpForm); }
    );
  }



}
