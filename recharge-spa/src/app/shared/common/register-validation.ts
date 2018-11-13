import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

import { AuthService } from '../_services/auth.service';

@Injectable()
export class RegisterValidation {

    constructor(private authService: AuthService) {
    }

    confirm = (first: string, second: string) => {
        return (g: AbstractControl): ValidationErrors | null => {
            if (!g.parent) { return null; }

            const password: string = g.parent.get(first).value;
            const confirmPassword: string = g.parent.get(second).value;

            if (password && confirmPassword && password.toLowerCase() !== confirmPassword.toLowerCase()) {
                return {invalidConfirmPassword: true};
            }


            return null;
        };
    }

    strLength = (requiredLength: number) => {
        return (c: AbstractControl): ValidationErrors | null => {
            if (c.value && typeof c.value !== 'string') { return null; }

            const value: string = c.value;
            if (value.length !== requiredLength) {
                return {invalidLenght: true};
            }

            return null;
        };
    }

    stringRange = (min: number, max: number) => {
        return (c: AbstractControl): ValidationErrors | null => {

            const value: string = c.value + '';

            if (value.length < min || value.length > max) {
                return {invalidStringRange: true};
            }

            return null;
        };
    }

    numeric (c: AbstractControl): ValidationErrors | null {
        const value: string = c.value + '';
        const reg = new RegExp('^[\\+\\-]?\\d*\\.?\\d+(?:[Ee][\\+\\-]?\\d+)?$');

        if (!reg.test(value) ) {
            return {invalidNumber: true};
        }

        return null;
    }

    shouldBeUnique(c: AbstractControl): Promise<ValidationErrors | null> {
        return new Promise((resolve, reject) => {
            // resolve({ unique: true });
            // setTimeout(() => {
            // }, 3000);
            this.authService.exists(c.value).pipe(debounceTime(3000), distinctUntilChanged()).subscribe(x => {
                if (x) {resolve({ unique: true }); } else {resolve(null); }
            }, error => console.log('error'));
        });
    }
}
