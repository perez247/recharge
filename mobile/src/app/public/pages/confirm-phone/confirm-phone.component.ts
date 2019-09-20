import { Component, OnInit } from '@angular/core';
import { AppRoutes } from 'src/app/shared/routes/app.routes';
import { AppToken } from 'src/app/shared/app-domain/app-token';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from 'src/app/shared/state.management/store';

@Component({
  selector: 'app-confirm-phone',
  templateUrl: './confirm-phone.component.html',
  styleUrls: ['./confirm-phone.component.scss'],
})
export class ConfirmPhoneComponent implements OnInit {

  appRoutes = AppRoutes.generateRoutes();

  appToken: AppToken;
  changePhoneNumber = false;

  constructor(
    private redux: NgRedux<IAppState>,
  ) { }

  ngOnInit() {
    this.getMobileExpiry();
  }

  getMobileExpiry() {
    this.redux.getState().user.token.then((token: AppToken) => {
      console.log(token);
    });

  }

}
