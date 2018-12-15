import { MobileValidation } from './../../../shared/common/custom-validation/mobile-validation';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { RegisterValidation } from '../../../shared/common/custom-validation/register-validation';
import { RechargeService } from '../../../shared/_services/recharge.service';

@Component({
  selector: 'app-mobile',
  templateUrl: './mobile.page.html',
  styleUrls: ['./mobile.page.scss'],
})
export class MobilePage {

  topUpForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private registerValidation: RegisterValidation,
    private mobileValidation: MobileValidation,
    private rechargeService: RechargeService
  ) {
    this.initForm();
    this.topUpForm.valueChanges.subscribe(x => {
      this.rechargeService.refreshTypeData(this.topUpForm);
    });
  }

  initForm() {
    this.topUpForm = this.fb.group({
      phoneNumber: ['', [Validators.required, this.registerValidation.numeric, this.registerValidation.stringRange(10, 12)]],
      network: ['', [Validators.required, this.mobileValidation.requiredNetworks]],
      amount: ['', [Validators.required, this.registerValidation.numeric, this.mobileValidation.NumberRange(100, 50000)]],
      type: ['mobile', Validators.required]
    });
    this.rechargeService.refreshTypeData(this.topUpForm);
  }

}
