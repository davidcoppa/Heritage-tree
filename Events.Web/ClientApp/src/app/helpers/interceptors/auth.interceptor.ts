import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppAuthService } from '../../server/app.auth';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AppAuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add authorization header with basic auth credentials if available
      const currentUser = this.authenticationService.currentUserValue;
      if (currentUser && currentUser.token) {
            request = request.clone({
              setHeaders: {
                Authorization: `Bearer ${currentUser.token}`
                }
            });
        }

        return next.handle(request);
    }
}
