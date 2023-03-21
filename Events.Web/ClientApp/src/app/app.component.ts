import { BreakpointObserver } from '@angular/cdk/layout';
import { AfterContentInit,  Component,  ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import {  filter } from 'rxjs/operators';
//import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { NavigationEnd, Router } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterContentInit {
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;


  title = 'Heritage Tree';

  constructor(
    private observer: BreakpointObserver,
    private router: Router) {

  }
  ngAfterContentInit() {
    //  console.log("sidenav mode")
    if (this.sidenav == undefined) { return; }
    this.observer
      .observe(['(max-width: 800px)'])
      //.pipe(delay(1), untilDestroyed(this))
      .subscribe((val: any) => {
        if (val.matches) {

          this.sidenav.mode = 'over';
          this.sidenav.close();
        } else {
          this.sidenav.mode = 'side';
          this.sidenav.open();
        }
      });

    this.router.events
      .pipe(
        // untilDestroyed(this),
        filter((e) => e instanceof NavigationEnd)
      )
      .subscribe(() => {
        if (this.sidenav.mode === 'over') {
          this.sidenav.close();
        }
      });
  }


}
