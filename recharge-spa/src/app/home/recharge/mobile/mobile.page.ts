import { MobileValidation } from './../../../shared/common/custom-validation/mobile-validation';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { RegisterValidation } from '../../../shared/common/custom-validation/register-validation';

@Component({
  selector: 'app-mobile',
  templateUrl: './mobile.page.html',
  styleUrls: ['./mobile.page.scss'],
})
export class MobilePage implements OnInit {

  topUpForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private registerValidation: RegisterValidation,
    private mobileValidation: MobileValidation
  ) {
    this.initForm();
  }

  ngOnInit() {
  }

  initForm() {
    this.topUpForm = this.fb.group({
      phoneNumber: ['', [Validators.required, this.registerValidation.numeric, this.registerValidation.stringRange(10, 12)]],
      network: ['', [Validators.required, this.mobileValidation.requiredNetworks]],
      amount: ['', [Validators.required, this.registerValidation.numeric, this.mobileValidation.NumberRange(100, 50000)]],
      type: ['mobile', Validators.required]
    });
  }

}
