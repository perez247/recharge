import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import * as lodash from 'lodash';
import { ServerError } from '../../interceptors/app-error-handler';

@Injectable({
  providedIn: 'root'
})
export class AppErrorService {

  constructor() { }


  setError(error: ServerError, reactiveForm: FormGroup) {
    if (error.error) {
        // this.toast.error(error.error);
        console.log(error);
        return;

    } else if (!lodash.isEmpty(error.errors)) {

        const errors = new Object(error.errors);
        // console.log(errors);

        Object.keys(errors).forEach((props: string) => {
            reactiveForm.controls[props].setErrors( { message: errors[props][0], serverError: true  } );
        });
        return reactiveForm;
    }

    console.log('An error occurred, please try again later');
    // this.toast.error('An error occurred, please try again later');
  }
}
