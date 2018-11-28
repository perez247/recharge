import { PaymentValidation } from './../../shared/common/custom-validation/payment-validation';
import { RegisterValidation } from './../../shared/common/custom-validation/register-validation';
import { MobileValidation } from './../../shared/common/custom-validation/mobile-validation';
import { Component, Input, ChangeDetectorRef, AfterContentChecked } from '@angular/core';
import { RechargeService } from '../../shared/_services/recharge.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-payments',
  templateUrl: './payments.page.html',
  styleUrls: ['./payments.page.scss'],
})
export class PaymentsPage implements AfterContentChecked {
  @Input() outerFormData: any = {};
  @Input() outerFormValid: boolean;

  paymentouterFormData: any = {};
  validTotal = false;

  newCard = false;
  paymentForm: FormGroup;

  constructor(
    private rechargeService: RechargeService,
    private cdref: ChangeDetectorRef,
    private fb: FormBuilder,
    private mobileValidation: MobileValidation,
    private registerValidation: RegisterValidation,
    private paymentValidation: PaymentValidation
  ) {
    this.initForm();
   }

  process() {
    const data = {...this.outerFormData, payment: this.paymentForm.value};
    // console.log(data);
    // console.log(this.paymentForm);
    this.rechargeService.recharge(data, this.outerFormData.type).subscribe(x => {
      console.log(x);
    }, error => {console.log(error); } );
  }

  setTwoNumberDecimal($event) {
    $event.target.value = $event.target.value ? parseFloat($event.target.value).toFixed(2) : 0.00;
  }

  Totalled() {
    const points = +this.paymentForm.get('point').value || 0;
    const card = +this.paymentForm.get('cardAmount').value || 0;
    const total = points + card;

    if (+total !== 0 && +total === +this.outerFormData.amount) { this.validTotal = true; } else { this.validTotal = false; }

    return +total;
  }

  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  getNumber(data) {
    return (typeof +data === 'number') ? +data : 0;
  }

  toggleCardForm() {
    this.newCard = !this.newCard;
    this.updateform(this.noRequiredCard);
  }

  CardForm(e) {
    // console.log(e.value);
    if (e.value === 'new') {
      this.toggleCardForm();
      this.updateform(this.requiredCard);
    } else {
      this.newCard = false;
      this.updateform(this.noRequiredCard);
    }
  }

  initForm() {
    this.paymentForm = this.fb.group({
      point: ['', [Validators.pattern('^[0-9]*$'), this.paymentValidation.NullOrNumberRange(100, 500000)]],
      cardId: ['', [Validators.required]],
      cardAmount: ['',
                      [
                        Validators.pattern('^[0-9]*$'),
                        this.paymentValidation.NullOrNumberRange(100, 500000)
                      ]
                  ],
      pin: ['', [Validators.required, Validators.pattern('^[0-9]{8}$')]],
      newCard: this.noRequiredCard()
    });
  }


  noRequiredCard() {
    const formgroup = this.fb.group({
      carNumber: [''],
      cardHolderName: [''],
      expiryDate: [''],
      cvvNumber: [''],
      saveCard: [false]
    });

    return formgroup;
  }

  requiredCard() {
    const cardNoRegex =
      `^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|
      3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$`;

    const formgroup = this.fb.group({
      cardNumber: ['', [Validators.required, Validators.pattern(cardNoRegex)]],
      cardHolderName: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9-_ ]+$')]],
      expiryMonth: ['', [Validators.required, Validators.pattern('([1-9]|10|11|12)$')]],
      expiryYear: ['', [Validators.required, Validators.pattern('20[0-9]{2}$')]],
      cvvNumber: ['', [Validators.required, Validators.pattern('[0-9]{3}$')]],
      saveCard: [false, [Validators.required]]
    });

    return formgroup;
  }

  updateform(control: () => any) {
    this.paymentForm.setControl('newCard', control.apply(this));
    this.paymentForm.updateValueAndValidity();
  }

  formValid() {
    const f = this.outerFormValid;
    const f2 = this.paymentForm.valid;
    const t = this.validTotal;
    return !(f && f2 && t);
  }

  defaultTo(event, value) {
    if (event.target.value.lenght === 0) {
      event.target.value = value;
    }
  }

  generateArray(from: number, to: number) {
    const arr: number[] = [];
    for (let index = from; index <= to; index++) {
      arr.push(index);
    }

    return arr;
  }

}
