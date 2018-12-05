import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-down-links',
  templateUrl: './down-links.page.html',
  styleUrls: ['./down-links.page.scss'],
})
export class DownLinksPage {
  viewName = 'myDownLink';
  viewNames: string[] = ['myDownLink', 'myTransactions', 'otherTransactions'];

  constructor() { }

  changeView(viewName: string) {
    if (this.viewNames.indexOf(viewName) > -1) {
      this.viewName = viewName;
    } else { this.viewName = 'myDownLink'; }
    // console.log(this.viewNames.indexOf(viewName) > -1);
  }
}
