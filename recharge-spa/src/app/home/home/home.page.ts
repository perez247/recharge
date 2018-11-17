import { HomeService } from './../../shared/_services/home.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage {

  constructor(private homeService: HomeService) { }

  ionViewWillEnter() {
    this.homeService.get().subscribe(x => {
      console.log(x);
    });
  }

}
