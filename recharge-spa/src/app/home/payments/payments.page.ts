import { Component, Output, EventEmitter, Input } from '@angular/core';
import { RechargeService } from '../../shared/_services/recharge.service';

@Component({
  selector: 'app-payments',
  templateUrl: './payments.page.html',
  styleUrls: ['./payments.page.scss'],
})
export class PaymentsPage {
  @Input() formData: any;
  @Input() formInvalid: boolean;

  paymentFormData: any = {};

  constructor(
    private rechargeService: RechargeService
  ) { }

  process() {
    this.rechargeService.recharge(this.formData, this.formData.type).subscribe(x => {
      console.log(x);
    }, error => {console.log(error); } );
  }

  setTwoNumberDecimal($event) {
    $event.target.value = $event.target.value ? parseFloat($event.target.value).toFixed(2) : 0.00;
    console.log($event.target.value);
  }
}
