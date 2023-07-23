import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../model/user';

@Injectable({ providedIn: 'root' })
export class AppAuthService {
  private currentUserSubject: BehaviorSubject<User>
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser') || '{}'));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(Email: string, Password: string, CaptchaToken:string) {

    return this.http.post<any>('api/Account/Login', { Email, Password, CaptchaToken }, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    })
      .pipe(
        map(
          user => {
            // store user details and basic auth credentials in local storage to keep user logged in between page refreshes
            user.authdata = window.btoa(Email + ':' + user.token);
            localStorage.setItem('jwt', JSON.stringify(user.token));
            this.currentUserSubject.next(user);
            return user;
          }),
        //catchError(this.handleError)

      );
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('jwt');
    this.currentUserSubject.next(new User());
  }


  register(Email: string, Password: string, Password2: string, CaptchaToken: string): Observable<any> {
    return this.http.post("api/Account/Create", { Email, Password, Password2, CaptchaToken });
  }


}

