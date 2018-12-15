import { ToasterService } from './../../shared/_services/toaster.service';
import { TokenService } from './../../shared/_services/token.service';
import { AppCard } from './../../shared/model/app-card';
import { AfterContentChecked, ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { RechargeService } from '../../shared/_services/recharge.service';
import { PaymentValidation } from './../../shared/common/custom-validation/payment-validation';
import { AppUser } from '../../shared/model/app-user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payments',
  templateUrl: './payments.page.html',
  styleUrls: ['./payments.page.scss'],
})
export class PaymentsPage implements AfterContentChecked {

  paymentouterFormData: any = {};
  validTotal = false;
  userData = {
    point: 0.0,
    cards: [] as AppCard[]
  };

  newCard = false;
  paymentForm: FormGroup;
  outerFormData: FormGroup;

  constructor(
    private rechargeService: RechargeService,
    private cdref: ChangeDetectorRef,
    private fb: FormBuilder,
    private paymentValidation: PaymentValidation,
    private tokenService: TokenService,
    private toastService: ToasterService,
    private router: Router
  ) {
    this.initForm();
    this.rechargeService.typeData.subscribe(typeData => {
      this.outerFormData = typeData;
    });
    this.rechargeService.getUser().subscribe((x: any) => {
      this.userData.point = x.point.points;
      this.userData.cards = x.cards;
      this.initForm();
    });
   }

  process() {
    const data = {...this.outerFormData.value, payment: this.paymentForm.value};
    // console.log(data);
    // console.log(this.paymentForm);
    this.rechargeService.recharge(data, this.outerFormData.get('type').value).subscribe((x: any) => {
      this.tokenService.save(x.token, x.user as AppUser);
      this.clear();
      this.toastService.shout('Payment', 'Transaction Successfull', () => {
        this.router.navigate(['home']);
      });
    });
  }

  setTwoNumberDecimal($event) {
    $event.target.value = $event.target.value ? parseFloat($event.target.value).toFixed(2) : 0.00;
  }

  Totalled() {
    const points = +this.paymentForm.get('point').value || 0;
    const card = +this.paymentForm.get('cardAmount').value || 0;
    const total = points + card;

    if (+total !== 0 && +total === +this.outerFormData.get('amount').value) { this.validTotal = true; } else { this.validTotal = false; }

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
      point: ['', [
        Validators.pattern('^[0-9]*$'),
        this.paymentValidation.NullOrNumberRange(100, 500000),
        this.paymentValidation.checkPoint(this.userData.point)
        ]
      ],
      cardId: ['', []],
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
      cardNumber: [''],
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
    const f = this.outerFormData.valid;
    const f2 = this.paymentForm.valid;
    const t = this.validTotal;
    const points = this.userData.point >= this.paymentForm.get('point').value;
    return !(f && f2 && t && points);
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

  clear() {
    this.initForm();
    this.outerFormData.reset();
  }
}
