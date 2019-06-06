import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.page.html',
  styleUrls: ['./auth.page.scss'],
})
export class AuthPage implements OnInit, OnDestroy {

  subscription: Subscription;
  state = 'signin';
  availableState = ['sigin', 'signup', 'confirm-phone'];

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.checkUrls();
  }

  checkUrls() {
    this.subscription = this.route.params.subscribe((e: any) => {
      this.state = (this.availableState.indexOf(e.state) > -1) ? e.state : 'signin';
    });
    // this.subscription = this.router.events
    // .pipe(
    //   filter(event => event instanceof RoutesRecognized),
    //   pairwise()
    // )
    // .subscribe((e: any) => {
    //     console.log(e);
    //     // if (e[0].urlAfterRedirects !== e[1].urlAfterRedirects && e[1].urlAfterRedirects !== '/auth') {
    //     //   this.redux.dispatch( {type: UIActionConstant.LOADING_RIGHT_CONTENT, loading: true} );
    //     // }
    // });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}


