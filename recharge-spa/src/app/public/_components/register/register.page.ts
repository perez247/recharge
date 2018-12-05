import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { RegisterValidation } from '../../../shared/common/custom-validation/register-validation';
import { AuthService } from '../../../shared/_services/auth.service';
import { TokenService } from '../../../shared/_services/token.service';
import { AppUser } from '../../../shared/model/app-user';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
})
export class RegisterPage {
  @Output() registered = new EventEmitter();

  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private registerValidation: RegisterValidation,
    private tokenService: TokenService,
    private router: Router
    ) {
    this.initForm();
   }

   ionViewCanEnter() {
   }

   ionViewWillEnter() {
    // this.initForm();
   }

  initForm() {
    this.registerForm = this.fb.group({
      userName: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9-_]+$')],
                      this.registerValidation.shouldBeUnique.bind(this.registerValidation)],
      phoneNumber: ['',
                    [
                      Validators.required,
                      this.registerValidation.numeric,
                      this.registerValidation.stringRange(10, 12),
                    ],
                    [
                      this.registerValidation.shouldBeUnique.bind(this.registerValidation)
                    ]
                  ],
      pin: ['',
                    [
                      Validators.required,
                      this.registerValidation.numeric,
                      this.registerValidation.strLength(8)
                    ]
                ],
      confirmPin: ['', [Validators.required, this.registerValidation.confirm('pin', 'confirmPin')]],
      referer: ['', [Validators.pattern('^[a-zA-Z0-9-_]+$')]]
    });
  }


  registerUser() {
    this.authService.register(this.registerForm.value).subscribe(x => {
      // this.registered.emit();
      this.tokenService.save(x.token, x.user as AppUser);
      this.initForm();
      this.router.navigate(['confirm-phone'], {
        queryParams : {
          userId: x.user.id,
          code: x.user.code
        }
      });
    }, error => console.log(error));
  }


}
