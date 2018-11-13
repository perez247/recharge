import { AuthService } from '../../../shared/_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-confirm-phone',
  templateUrl: './confirm-phone.page.html',
  styleUrls: ['./confirm-phone.page.scss'],
})
export class ConfirmPhonePage {

  userId: string;
  resendButton = false;
  timer = 0;

  constructor(
    public authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
    ) {}

  ionViewWillEnter() {
    this.userId = this.route.snapshot.queryParamMap.get('userId');
    const code = this.route.snapshot.queryParamMap.get('code');

    console.log(this.userId);
    console.log(code);
    // this.countDown();
    // console.log('hello');
  }

  sendConfirmation(f: NgForm) {
    const data = {code: f.value.code, id: this.userId};
    this.authService.confirm(data).subscribe(result => {
      if (result) {
        f.reset();
        this.redirect();
      } else {console.log(result); }
    }, error => console.log(error));
  }

  resend() {
    this.countDown();
    this.authService.resendCode(this.userId).subscribe(code => {
      console.log(code);
    }, error => console.log(error));
  }

  countDown() {
    this.resendButton = true;
    this.timer = 60;
    const timer = setInterval(() => {
      this.timer--;
      if (this.timer <= 0) {
        clearInterval(timer);
        this.resendButton = false;
      }
    }, 1000);
  }

  redirect() {
    // You can check if the user is logged in so you redirect to home page
    // else to login page for now redirect to login page;
    this.router.navigate(['auth']);
  }

}
