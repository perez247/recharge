import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RequestValidations } from 'src/app/shared/validations/request-validations';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {

  signUpForm: FormGroup;

  constructor(private fb: FormBuilder, private requestV: RequestValidations) { }

  ngOnInit() {
    this.initializeSignUpForm();
  }

  initializeSignUpForm() {
    this.signUpForm  = this.fb.group({
      username : [null, [Validators.required], [this.requestV.unique(null)]],
      countryCode : [null, Validators.required],
      phoneNumber : [null, Validators.required],
      pin : [null, Validators.required],
      confirmPin : [null, Validators.required],
      referersCountryCode : [null],
      referersPhoneNumber : [null]
    });
  }

  SignUp() {
    console.log(this.signUpForm.value);
  }

}
