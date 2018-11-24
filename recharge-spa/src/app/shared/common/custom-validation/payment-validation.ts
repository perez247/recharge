import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';

@Injectable()
export class PaymentValidation {

    constructor() {
    }

    checkTotal = (total: string, cardAmount: string, point: string) => {
        return (g: AbstractControl): ValidationErrors | null => {
            if (!g.parent) { return null; }

            // const totalValue = g.parent.get('total')

            // console.log(totalValue);

            // if (!total.amount) { return null; }

            // const pointValue: number = g.parent.get(cardAmount).value;
            // const cardAmountValue: number = g.parent.get(point).value;

            // console.log(total);

            // if (total.amount > 2000) {
            //     return {invalidTotal: true};
            // }
            return null;
        };
    }

    NullOrNumberRange = (minAmount: number, maxAmount: number) => {
        return (c: AbstractControl): ValidationErrors | null => {

            if (!c.value) {
                return null;
            }

            const value: number = c.value ;

            if (value > maxAmount || value < minAmount) {
                return {invalidNullorNumberRange: true};
            }

            return null;
        };
    }
}
