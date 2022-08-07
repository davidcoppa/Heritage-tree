import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class CustomDateAdapterService {


  CalibrateDate(date: Date): Date {

    if (date.getTimezoneOffset == undefined) {
      return date;
    }
    return new Date(Date.parse(date.toString()) - (date.getTimezoneOffset() * 60000))
  }
}
