import { ActivatedRoute, Router } from '@angular/router';
import { TokenService } from './../../../shared/_services/token.service';
import { AppUser } from './../../../shared/model/app-user';
import { AuthService } from '../../../shared/_services/auth.service';
import { Component, } from '@angular/core';
import { ToasterService } from '../../../shared/_services/toaster.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage {
  user: any = {};
  private returnUrl = 'home';

  constructor(
    private authService: AuthService,
    private tokenService: TokenService,
    private route: ActivatedRoute,
    private router: Router,
    private toast: ToasterService
    ) { }

  login() {
    // console.log(this.user);
    this.authService.login(this.user).subscribe((x: any) => {
      // console.log(x);
      this.tokenService.save(x.token, x.user as AppUser);
      this.redirect();
    });
  }

  redirect() {
    const url = this.route.snapshot.queryParamMap.get('returnUrl') || this.returnUrl;
    this.router.navigate([url]);
  }

}
