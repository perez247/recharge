import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-logo-heading',
  templateUrl: './logo-heading.component.html',
  styleUrls: ['./logo-heading.component.scss'],
})
export class LogoHeadingComponent implements OnInit {

  @Input() field: string;

  constructor() { }

  ngOnInit() {}

}
