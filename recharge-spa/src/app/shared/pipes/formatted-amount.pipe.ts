import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formattedAmount'
})
export class FormattedAmountPipe implements PipeTransform {
  bonusPercentage = 0.05;

  transform(value: string, bonus?: boolean): any {
    const v = parseFloat(value);
    if (!v) {
        return '0.00';
      }

    let number = Math.round(v * 100) / 100;

    if (bonus) {
      number *= this.bonusPercentage;
    }

    return parseFloat(number + '').toFixed(2);
  }

}
