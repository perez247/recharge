import { Component, OnInit } from '@angular/core';
import { AppRoutes } from 'src/app/shared/routes/app.routes';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss'],
})
export class SigninComponent implements OnInit {

  appRoutes = AppRoutes.generateRoutes();

  constructor() { }

  ngOnInit() {}

}
