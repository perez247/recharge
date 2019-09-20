import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';

import { debounceTime, distinctUntilChanged, finalize } from 'rxjs/operators';
import { AuthService } from '../services/auth/auth.service';

@Injectable()
export class RequestValidations {

    constructor(private authService: AuthService) {

    }

    unique = (field: string) => {
        return (c: AbstractControl): Promise<ValidationErrors | null> => {

            return new Promise((resolve, reject) => {

                c.valueChanges.pipe(
                    debounceTime(3000), distinctUntilChanged())
                    .subscribe((data) => {

                        this.authService.unique(data)
                        .subscribe(result => {
                            console.log(result);
                            if (result) {resolve(null); } else { resolve({ unique: true }); }
                        });

                    });

            });

        };
    }
}
