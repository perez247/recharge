import { ActivatedRoute, Router } from '@angular/router';
import { TokenService } from './../../../shared/_services/token.service';
import { AppUser } from './../../../shared/model/app-user';
import { AuthService } from '../../../shared/_services/auth.service';
import { Component, } from '@angular/core';

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
    private router: Router
    ) { }

  login() {
    // console.log(this.user);
    this.authService.login(this.user).subscribe((x: any) => {
      // console.log(x.token);
      this.tokenService.save(x.token);
      this.redirect();
    }, error => {console.log('failed'); });
  }

  redirect() {
    const url = this.route.snapshot.queryParamMap.get('returnUrl') || this.returnUrl;
    this.router.navigate([url]);
  }

}
