import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';

import { AuthService } from '../../_services/auth.service';

@Injectable()
export class MobileValidation {

    constructor(private authService: AuthService) {
    }



    NumberRange = (minAmount: number, maxAmount: number) => {
        return (c: AbstractControl): ValidationErrors | null => {

            const value: number = c.value ;

            if (value > maxAmount || value < minAmount) {
                return {invalidNumberRange: true};
            }

            return null;
        };
    }

    requiredNetworks (c: AbstractControl): ValidationErrors | null {
        const networks = ['mtn', 'glo', '9mobile', 'airtel'];

        if (networks.indexOf(c.value) === -1) {
            return {invalidNetwork: true};
        }

        return null;
    }
}
