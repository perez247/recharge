import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { SignupComponent } from './pages/signup/signup.component';
import { SigninComponent } from './pages/signin/signin.component';
import { ConfirmPhoneComponent } from './pages/confirm-phone/confirm-phone.component';

@NgModule({
    declarations: [
        SignupComponent,
        SigninComponent,
        ConfirmPhoneComponent
    ],
    imports: [
        SharedModule
    ],
    exports: [
        SignupComponent,
        SigninComponent,
        SharedModule,
        ConfirmPhoneComponent
    ]
})

export class PublicSharedModule { }
