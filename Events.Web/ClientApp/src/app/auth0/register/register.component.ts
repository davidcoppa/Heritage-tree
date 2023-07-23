import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { AppAuthService } from '../../server/app.auth';
import { EqualStringValidator } from '../../helpers/validators/equalStringValidator';
import { ReCaptchaV3Service } from 'ng-recaptcha';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form: FormGroup;
  submitted = true;
  loading = false;
  error: '';
  token: string;
  registerOk = false;

  constructor(private router: Router,
    private formBuilder: FormBuilder,
    private newUserService: AppAuthService,
    private recaptchaV3Service: ReCaptchaV3Service
  ) {
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{11,}'), Validators.minLength(12)]],
      password2: ['', Validators.required]
    }
      , {
      validator: EqualStringValidator('password', 'password2')

      }
    );
  }
  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }
    this.loading = true;


    this.recaptchaV3Service.execute('importantAction')
      .subscribe((token: string) => {
        console.debug(`Token [${token}] generated`);
        this.token = token;

        this.newUserService.register(this.f['email'].value, this.f['password'].value, this.f['password2'].value, this.token)
          .pipe()
          .subscribe(
            (data: any) => {
              //add email confirmation?
              //this.redirect();
              this.registerOk = true;
            },
            (err) => {
              this.loading = false;
              this.error = err;

            }, () => {
           //   this.router.navigate(['/login'], { queryParams: { email: this.f['email'].value, } });
            });

      });

  }

  goToLogin() {
    this.router.navigate(['/login'], { queryParams: { email: this.f['email'].value, } });
  }

  

}



