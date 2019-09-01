import { NgRedux } from '@angular-redux/store';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ServerError } from 'src/app/shared/interceptors/app-error-handler';
import { AppRoutes } from 'src/app/shared/routes/app.routes';
import { AuthService } from 'src/app/shared/services/auth/auth.service';
import { AppErrorService } from 'src/app/shared/services/errors/app-error.service';
import { IAppState } from 'src/app/shared/state.management/store';
import { UserActionConstant } from 'src/app/shared/state.management/user-state/user-action-constant';
import { CoreValidation } from 'src/app/shared/validations/core-validation';
import { CustomValidator } from 'src/app/shared/validations/custom-validation';
import { MobileValidation } from 'src/app/shared/validations/mobile-validation';

import * as _ from 'lodash';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {

  appRoutes = AppRoutes.generateRoutes();

  signUpForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private errorService: AppErrorService,
    private redux: NgRedux<IAppState>,
    private router: Router,
    ) { }

  ngOnInit() {
    this.initializeSignUpForm();
  }

  initializeSignUpForm() {
    this.signUpForm  = this.fb.group({
      countryCode : [null, CustomValidator.CustomRequired('Country Code')],
      phoneNumber : [null, [CustomValidator.CustomRequired('Phone Number'), MobileValidation.validPhoneNumber('countryCode')]],
      pin : [null, [CustomValidator.CustomRequired('Pin'), CoreValidation.ValidPin]],
      confirmPin : [null, [CustomValidator.CustomRequired('Confirm Pin'), CoreValidation.confirmation('pin')]],
      referersCountryCode : [null],
      referersPhoneNumber : [null, MobileValidation.validPhoneNumber('referersCountryCode', false)]
    });
  }

  SignUp() {
    this.authService.signUp(this.signUpForm.value).subscribe((x: any) => {
      this.redux.dispatch({ type: UserActionConstant.SAVE_AUTH_USER, payload: x.token });
      // console.log('created', x);

      this.redux.select((token) => token.user.token).subscribe(storedTokenPromise => {
        storedTokenPromise
          .then(storedToken => {

            if (!_.isEmpty(storedToken)) {
              this.router.navigate([this.appRoutes.private.confirmPhone.path]);
            } else {
              console.log('it seems something went wrong');
            }

          })
          .catch((e) => { console.log(e, 'it seems something went wrong'); } );
      });

      // this.redux.getState().user.token
      //   .then(() => { this.router.navigate([this.appRoutes.private.confirmPhone.path]); })
      //   .catch((e) => { console.log(e, 'it seems something went wrong'); });

    },
      (error: any) => { this.errorService.setError(error.error as ServerError, this.signUpForm); }
    );
  }



}
