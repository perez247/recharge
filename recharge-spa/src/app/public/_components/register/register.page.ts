import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { RegisterValidation } from '../../../shared/common/custom-validation/register-validation';
import { AuthService } from '../../../shared/_services/auth.service';

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
    private router: Router) {
    this.initForm();
   }

   ionViewCanEnter() {
   }

   ionViewWillEnter() {
    // this.initForm();
   }

  initForm() {
    this.registerForm = this.fb.group({
      userName: ['', Validators.required, this.registerValidation.shouldBeUnique.bind(this.registerValidation)],
      phoneNumber: ['',
                    [
                      Validators.required,
                      this.registerValidation.numeric,
                      this.registerValidation.stringRange(8, 12),
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
                    ],
                    []
                ],
      confirmPin: ['', [Validators.required, this.registerValidation.confirm('pin', 'confirmPin')]],
      referer: ['']
    });
  }


  registerUser() {
    this.authService.register(this.registerForm.value).subscribe(x => {
      // this.registered.emit();
      this.initForm();
      this.router.navigate(['confirm-phone'], {
        queryParams : {
          userId: x.id,
          code: x.code
        }
      });
    }, error => console.log(error));
  }


}
