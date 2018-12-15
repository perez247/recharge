import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';

@Injectable()
export class PaymentValidation {

    constructor() {
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

    checkPoint = (point: number) => {
        return (c: AbstractControl): ValidationErrors | null => {

            if (!c.value) {
                return null;
            }

            const value: number = c.value ;

            if (value > point ) {
                return {invalidCheckPoint: true};
            }

            return null;
        };
    }

    // checkPoint = (point: number) => {
    //     return  (c: AbstractControl): Promise<ValidationErrors | null> => {
    //         return new Promise((resolve, reject) => {
    //             if (!c.value) {
    //                 return resolve(null);
    //             }

    //             const value: number = c.value ;

    //             if (value > point ) {
    //                 resolve({invalidCheckPoint: true});
    //             }

    //             return resolve(null);
    //         });
    //     };
    // }
}
