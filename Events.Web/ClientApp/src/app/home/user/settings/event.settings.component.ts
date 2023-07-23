import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { AppAuthService } from '../../server/app.auth';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {

  constructor(private router: Router,
    private formBuilder: FormBuilder,
    private jwtHelper: JwtHelperService,
    private toastr: ToastrService,
    private authenticationService: AppAuthService) {
    //if (this.authenticationService.currentUserValue.authdata != undefined) {
    //  this.router.navigate(['/people']);
    //}
  }
 // error: string;
  loginForm: FormGroup;
  invalidLogin?: boolean = true;
  loading: boolean;

  ngOnInit() {
    this.loginForm = new FormGroup({
      username: new FormControl(''),
      password: new FormControl(''),
    });
  }
  error: string;

  //@Output() submitEM = new EventEmitter();

  get f() { return this.loginForm.controls; }

  onSubmit() {

    //add captcha
    if (this.loginForm.invalid) {
      return;
    }
    if (this.f['username'].value == '' || this.f['password'].value =='') {
      return;
    }


    this.loading = true;

    this.authenticationService.login(this.f['username'].value, this.f['password'].value)
      .subscribe(userData => {
        this.router.navigate(["/people"]);

      }, err => {
        this.loading = false;
        this.invalidLogin = true;
        this.error = 'oh no!!!';
      })



  }


  //isUserAuthenticated() {
  //  const token = localStorage.getItem("jwt");
  //  if (token && !this.jwtHelper.isTokenExpired(token)) {
  //    return true;
  //  }
  //  else {
  //    return false;
  //  }
  //}

}
