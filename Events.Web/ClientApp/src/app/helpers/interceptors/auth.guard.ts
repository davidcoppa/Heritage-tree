import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AppAuthService } from '../../server/app.auth';


@Injectable({ providedIn: 'root' })
export class AuthGuard  {
  constructor(
    private router: Router,
    private authenticationService: AppAuthService
  ) { }

  canActivate(route: ActivatedRouteSnapshot,
              state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    const currentUser = this.authenticationService.currentUserValue;

    if (currentUser) {
      // logged in so return true
      return true;
    }

    // not logged in so redirect to login page with the return url
    this.router.navigate(['./login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
