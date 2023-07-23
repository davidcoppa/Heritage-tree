import { Component, OnInit } from "@angular/core";
import { Captcha } from "../../model/captcha.model";


@Component({
  selector: 'app-captcha',
  templateUrl: './captcha.component.html'
})
export class CaptchaComponent implements OnInit {

  captchas: Captcha;

  siteKey: string
  errorCaptcha = false;


  ngOnInit(): void {

    //TODO: load from configuration
    //PRO
    //siteKey = "6Le6kqEUAAAAAKFp-en20t8ub36okc6oFAK1D97h";
    //PRE
    this.siteKey = "6Ldtk6EUAAAAAMxIawOCmLPKu5EEGrL0IVcnmiv_";
    this.captchas = new Captcha()
  }

  public resolved(captchaResponse: string): void {//Captcha {
    // console.log(`Resolved captcha with response: ${captchaResponse}`);

    if (captchaResponse == null || captchaResponse == undefined || captchaResponse == '') {
      this.captchas.Value = '';
      this.captchas.Status = false;
    }
    else {
      this.captchas.Value = captchaResponse;
      this.captchas.Status = true;
    }
    //  return this.captcha;
  }




  public onError(errorDetails: RecaptchaErrorParameters): void {
    this.captchas.Value = '';
    this.captchas.Status = false;
    //  console.log(`reCAPTCHA error encountered; details:`, errorDetails);
  }





}
