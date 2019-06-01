import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SignUpModel } from './signup-form-model';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {

  signUpForm: FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.initializeSignUpForm();
  }

  initializeSignUpForm() {
    this.signUpForm  = this.fb.group({
      username : [null, Validators.required],
      countryCode : [null, Validators.required],
      phoneNumber : [null, Validators.required],
      pin : [null, Validators.required],
      confirmPin : [null, Validators.required],
      referersCountryCode : [null],
      referersPhoneNumber : [null]
    });
  }

  SignUp() {
    console.log(new SignUpModel(this.signUpForm.value).createFormData());
  }

}
