import { RegisterValidation } from './../../shared/common/custom-validation/register-validation';
import { MobileValidation } from './../../shared/common/custom-validation/mobile-validation';
import { Component } from '@angular/core';
import { BankService } from '../../shared/_services/bank.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-withdraw',
  templateUrl: './withdraw.page.html',
  styleUrls: ['./withdraw.page.scss'],
})
export class WithdrawPage {
  banks: any[] = [];
  withdrawFrom: FormGroup;

  constructor(
    private bankService: BankService,
    private fb: FormBuilder,
    private mobileValidation: MobileValidation,
    private registerValidation: RegisterValidation
    ) {
      this.initForm();
     }

  ionViewDidEnter() {
    this.bankService.getBanks().subscribe(b => {
      this.banks = b;
    });
  }

  initForm() {
    this.withdrawFrom = this.fb.group({
      accountNumber: ['', [Validators.required, Validators.pattern('^[0-9]+$'), this.registerValidation.stringRange(10, 12)]],
      accountBank: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9-_ ]+$')]],
      accountName: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9-_ ]+$')]],
      amount: ['', [Validators.required, Validators.pattern('^[0-9]+$'), this.mobileValidation.NumberRange(100, 50000)]],
    });
  }
}
