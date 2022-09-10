import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first, Subscription } from 'rxjs';
import { AppService } from 'src/app/server/app.service';
import { LocationEnum } from '../../../../helpers/enums/location.enum';
import { Country } from '../../../../model/country.model';
import { ListObject } from '../../../../model/listObject.model';

@Component({
  selector: 'app-country-abm',
  templateUrl: './country-abm.component.html'
})
export class CountryAbmComponent implements OnInit, OnDestroy {
  @Input() countrySelected: Country;
  @Input() abmCountry: boolean;

  country: FormGroup;
  fb: FormBuilder;
  evt: Country;
  buttonAction: string;

  listModel: ListObject;

  private subscriptionCountryFilter: Subscription;

  constructor(fb: FormBuilder, private service: AppService) {

    this.fb = fb;

    this.subscriptionCountryFilter = this.service.getUpdateCountry().subscribe
      (data => { //message contains the data sent from service
        console.log("sendUpdate country: " + data.data);
        this.countrySelected = data.data;

      


      });
  }

  ngOnDestroy() {
    this.subscriptionCountryFilter.unsubscribe();
  }

  ngOnInit(): void {
    console.log("country edit init: ");

    if (this.countrySelected != undefined) {
      this.country = this.CreateForm(this.countrySelected);
    } else {
      this.country = this.CreateForm(null);
    }

  }

  CreateForm(countryEdit: Country | null): FormGroup {


    if (countryEdit == null) {

      this.buttonAction = "Add";

      return this.fb.group({
        name: [null, [Validators.required]],
        code: [null],
        capital: [null],
        region: [null],
        latitude: [null],
        longitude: [null]
      });
    } else {
      this.evt = this.countrySelected;
      this.buttonAction = "Update";

      return this.fb.group({
        name: new FormControl(countryEdit.name ?? null),
        code: new FormControl(countryEdit.code ?? null),
        capital: new FormControl(countryEdit.capital ?? null),
        region: new FormControl(countryEdit.region ?? null),
        latitude: new FormControl(countryEdit.lat ?? null),
        longitude: new FormControl(countryEdit.lgn ?? null)
      });
    }

  }

  ngOnChanges() {
    console.log("country on changes: ");

    if (this.countrySelected == null) {
      this.country = this.CreateForm(null);
    } else {
      this.evt = this.countrySelected;
    }
  }

  SaveCountry(CountryABM: FormGroup) {
    this.evt = CountryABM.value as Country;


    console.log('Current country abm: ', this.evt);

    if (this.buttonAction == "Update") {

      this.evt.id = (this.countrySelected).id;

      this.service.UpdateCountries((this.countrySelected).id, this.evt)
        .pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMCountryFinished();
          },
          error => console.log('Error Getting Position: ', error)
        );
    }
    else {

      this.service.AddCountries(this.evt).pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMCountryFinished();

          },
          error => console.log('Error Getting Position: ', error)
        );
    }
  }

  ABMCountryFinished() {
    this.service.sendUpdateCountry(true);
  }

  ResetForm() {
    this.country = this.CreateForm(null);
  }

  Cancel() {
    this.service.sendUpdateCountry(true);
  }

}


