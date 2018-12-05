import { ToasterService } from './../../shared/_services/toaster.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import * as moment from 'moment';

import { AuthService } from './../../shared/_services/auth.service';
import { HomeService } from './../../shared/_services/home.service';
import { AppUser } from './../../shared/model/app-user';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage {
  TimeRemaining: any;
  point: any = {};
  user: AppUser;

  constructor(
    private homeService: HomeService,
    private router: Router,
    private authService: AuthService,
    private toastService: ToasterService
    ) { }

  ionViewWillEnter() {
    this.homeService.get().subscribe((x: any) => {
      this.point.points = x.points;
    });

    // this.user = this.authService.user();

    // this.timeSpan();

    this.authService.setUser().subscribe(user => {
      if (user) {
        this.user = user;
        this.timeSpan();
      }
    });

  }

  goToType(e) {
    const url = e.target.value;
    e.target.value = '';
    if (url) {
      this.router.navigate([url]);
    }
  }

  timeSpan() {
    const data = moment.duration(moment(this.user.expires).diff(moment()));
    setInterval(() => {
      const duration = moment.duration(moment(this.user.expires).diff(moment()));
      this.TimeRemaining =
        `${duration.months()} month,
         ${this.checkday(duration.days())},
         ${duration.hours()}:${duration.minutes()}:${duration.seconds()}`;
    }, 1000);
  }

  ExpireInfo() {
    this.toastService.shout(
      'What is this?',
      `You have a timeframe of 60 days after your last recharge to recharge
      else you will not be recieving points from your downlinks`, null
    );
  }

  checkday(day: number) {
    return day <= 1 ? day + ' day' : day + ' days';
  }

}
