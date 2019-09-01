import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { SignupComponent } from './pages/signup/signup.component';
import { SigninComponent } from './pages/signin/signin.component';

@NgModule({
    declarations: [
        SignupComponent,
        SigninComponent,
    ],
    imports: [
        SharedModule
    ],
    exports: [
        SignupComponent,
        SigninComponent,
        SharedModule,
    ]
})

export class PublicSharedModule { }
