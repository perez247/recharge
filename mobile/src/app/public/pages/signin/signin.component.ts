import { Component, OnInit } from '@angular/core';
import { AppRoutes } from 'src/app/shared/routes/app.routes';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomValidator } from 'src/app/shared/validations/custom-validation';
import { MobileValidation } from 'src/app/shared/validations/mobile-validation';
import { CoreValidation } from 'src/app/shared/validations/core-validation';
import { AuthService } from 'src/app/shared/services/auth/auth.service';
import { AppErrorService } from 'src/app/shared/services/errors/app-error.service';
import { ServerError } from 'src/app/shared/interceptors/app-error-handler';
import { ToasterService } from 'src/app/shared/services/toaster/toaster.service';
import { Router } from '@angular/router';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from 'src/app/shared/state.management/store';
import { UserActionConstant } from 'src/app/shared/state.management/user-state/user-action-constant';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss'],
})
export class SigninComponent implements OnInit {

  appRoutes = AppRoutes.generateRoutes();

  signInForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private appError: AppErrorService,
    private toaster: ToasterService,
    private router: Router,
    private redux: NgRedux<IAppState>,
  ) { }

  ngOnInit() {
    this.initializeSigninForm();
  }

  initializeSigninForm() {
    this.signInForm = this.fb.group({
      countryCode: [null, CustomValidator.CustomRequired('Country Code')],
      phoneNumber: [null, [CustomValidator.CustomRequired('Phone Number'), MobileValidation.validPhoneNumber('countryCode')]],
      pin : [null, [CustomValidator.CustomRequired('Pin'), CoreValidation.ValidPin]],
    });
  }

  signIn() {
    this.authService.signIn(this.signInForm.value).subscribe((x: any) => {
      this.redux.dispatch({ type: UserActionConstant.SAVE_AUTH_USER, payload: x.token });
      this.toaster.success('login successfully');
      this.router.navigate([this.appRoutes.private.home.path]);
      // console.log(x);
    },
    async (error) => {await this.appError.setError(error.error as ServerError, this.signInForm); }
    );
  }

}
