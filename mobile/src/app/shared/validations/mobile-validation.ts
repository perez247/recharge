import { AbstractControl, ValidatorFn } from '@angular/forms';


export class MobileValidation {

    private static availableNumbers = {
        234 : /^([7-9]{1})([0|1]{1})([\d]{1})([\d]{7,8})$/g
    };

    static validPhoneNumber(countryCodeField: string, required = true): ValidatorFn {
        return (c: AbstractControl) => {
            const parent = c.parent;
            if (!parent) {
                return null;
            }

            if (!parent.get(countryCodeField).value) {
                return null;
            }

            if (!required && !c.value) {
                return null;
            }

            const countryCode = parent.get(countryCodeField).value;
            const pattern = MobileValidation.availableNumbers[countryCode] as RegExp;

            if (pattern.test(c.value)) {
                return null;
            } else {
                return {invalidPhoneNumber: true};
            }
        };
    }

}
