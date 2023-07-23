import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { AppAuthService } from '../../server/app.auth';
import { ReCaptchaV3Service } from 'ng-recaptcha';

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
    private authenticationService: AppAuthService,
    private recaptchaV3Service: ReCaptchaV3Service,
    private route: ActivatedRoute
  ) {
    //if (this.authenticationService.currentUserValue.authdata != undefined) {
    //  this.router.navigate(['/people']);
    //}
  }
  // error: string;
  loginForm: FormGroup;
  invalidLogin?: boolean = true;
  loading: boolean;
  token: string;
  error: string;


  ngOnInit() {
    var email: string='';
    this.route.queryParams
      .subscribe(params => {
        email = params['email'];

        this.loginForm = new FormGroup({
          username: new FormControl(email, [Validators.required, Validators.email]),
          password: new FormControl('', Validators.required),
        });

      });
      

   
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {

    //add captcha
    if (this.loginForm.invalid) {
      return;
    }
    if (this.f['username'].value == '' || this.f['password'].value == '') {
      return;
    }

    this.loading = true;

    this.recaptchaV3Service.execute('importantAction')
      .subscribe((token: string) => {
        console.debug(`Token [${token}] generated`);
        this.token = token;


        this.authenticationService.login(this.f['username'].value, this.f['password'].value, this.token)
          .subscribe(userData => {
            this.invalidLogin = false;
          }, err => {
            this.loading = false;
            this.invalidLogin = true;
            this.error = 'oh no!!!';
          },
            () => {
              this.router.navigate(["/people"]);
            }
        )

      });

  }

}
