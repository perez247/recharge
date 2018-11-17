import { HomeService } from './../../shared/_services/home.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage {

  constructor(
    private homeService: HomeService,
    private router: Router
    ) { }

  ionViewWillEnter() {
    this.homeService.get().subscribe(x => {
      console.log(x);
    });
  }

  goToType(e) {
    // console.log(e.target.value);
    const type = e.target.value || 'mobile';
    // const url  = `recharge/type/(${type}:${type})`;
    // console.log(url);
    this.router.navigate([type]);
  }

}
