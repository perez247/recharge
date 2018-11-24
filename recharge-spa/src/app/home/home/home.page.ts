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
    const url = e.target.value;
    e.target.value = '';
    this.router.navigate([url]);
  }

}
