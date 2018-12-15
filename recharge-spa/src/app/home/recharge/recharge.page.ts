import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-recharge',
  templateUrl: './recharge.page.html',
  styleUrls: ['./recharge.page.scss'],
})
export class RechargePage {
  type = [
    {value: 'mobile', name: 'Mobile Top-up'},
  ];

  selectedType: any = {};

  constructor(
    private route: ActivatedRoute
    ) { }

    ionViewWillEnter() {
      const param = this.route.snapshot.paramMap.get('type');
      this.selectedType = this.type.find(x => x.value === param);

      if (!this.selectedType) {
        this.selectedType = this.type.find(x => x.value === 'mobile');
      }
    }
}
