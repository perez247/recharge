import { WithdrawService } from './../../shared/_services/withdraw.service';
import { ToasterService } from './../../shared/_services/toaster.service';
import { RegisterValidation } from './../../shared/common/custom-validation/register-validation';
import { MobileValidation } from './../../shared/common/custom-validation/mobile-validation';
import { Component } from '@angular/core';
import { BankService } from '../../shared/_services/bank.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertController, ToastController } from '@ionic/angular';

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
    private registerValidation: RegisterValidation,
    private alertCtrl: AlertController,
    private toaster: ToasterService,
    private withdrawService: WithdrawService
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

  async proceed() {
    const alertctrl = await this.alertCtrl.create({
      header: 'Confirm Details',
      message: this.formattedFormValue(this.withdrawFrom.value),
      inputs: [
        {
          name: 'pin',
          type: 'password',
          placeholder: 'Secret Pin',
        }
      ],
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: (blah) => {
            console.log('Confirm Cancel: blah');
          }
        }, {
          text: 'Okay',
          handler: (data) => {
            if (this.validatePin(data.pin)) {
              this.makeWithdrawal(data.pin);
            } else {
              // alert('invalid pin');
              this.toaster.display('Invalid Pin', 'warning');
              return false;
            }
          }
        }
      ]
    });

    await alertctrl.present();
  }

  formattedFormValue(values: any) {
    const stringBuilder =
          `<strong>Account #: </strong> ${values.accountNumber} <br>
          <strong>Bank: </strong> ${this.bankName(values.accountBank)} <br>
          <strong>Name: </strong> ${values.accountName} <br>
          <strong>Amount: </strong> ${values.amount} <br>`;
    return stringBuilder;
  }

  bankName(bankName: string) {
    const bank = this.banks.find((x: any) => {
      return x.value === bankName;
    });

    if (bank) {
      return bank.name;
    }

    return '';
  }

  validatePin(pin) {
    return new RegExp('^[0-9]{8}$').test(pin);
  }

  makeWithdrawal(pin: string) {
    this.withdrawService.withdraw({...this.withdrawFrom.value, pin}).subscribe(result => {
      console.log(result);
    }, error => console.log(error));
  }

}
