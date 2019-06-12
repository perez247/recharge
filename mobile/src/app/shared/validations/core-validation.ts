import { AbstractControl, ValidatorFn } from '@angular/forms';

export class CoreValidation {


    static ValidPin(c: AbstractControl) {
        const pattern = new RegExp('^\\d{5}');

        if (pattern.test(c.value)) {
            return null;
        }

        return { invalidPin: true };
    }

    static confirmation(field: string): ValidatorFn {
        return (c: AbstractControl) => {
            const parent = c.parent;
            if (!parent) {
                return null;
            }

            const originalField = parent.get(field).value ?  parent.get(field).value : null;
            if (originalField === c.value) {
                return null;
            }

            return { invalidConfirmation: true };
        };
    }
}
