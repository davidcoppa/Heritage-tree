import { BreakpointObserver } from '@angular/cdk/layout';
import { AfterContentInit, AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { delay, filter } from 'rxjs/operators';
//import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { NavigationEnd, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterContentInit, OnInit {
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;


  title = 'Heritage Tree';
  isNewUser: boolean = false;
  constructor(
    private observer: BreakpointObserver,
    private router: Router,
    private jwtHelper: JwtHelperService)
  { }

  ngOnInit(): void {
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
  isUserAuthenticated() {
  //  localStorage.removeItem("jwt");
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      //   this.router.navigate(['/people']);
      
      return true;
    }
    else {
  //    this.router.navigate(['/login']);
      if (this.router.url.includes('register')) {
        this.isNewUser = true;
      }
      return false;
    }
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/login"]);
  }


}
