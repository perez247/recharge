import { HomeService } from './../../../shared/_services/home.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-transactions',
  templateUrl: './my-transactions.page.html',
  styleUrls: ['./my-transactions.page.scss'],
})
export class MyTransactionsPage implements OnInit {

  constructor(private homeService: HomeService) { }

  ngOnInit() {
    this.homeService.getUserTransactions().subscribe(transactions => {
      console.log(transactions);
    });
  }

  ionViewWillEnter() {
  }
}
