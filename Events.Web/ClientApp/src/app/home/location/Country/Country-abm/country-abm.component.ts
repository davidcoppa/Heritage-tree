import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { first, Subscription } from 'rxjs';
import { AppService } from 'src/app/server/app.service';
import { LocationEnum } from '../../../../helpers/enums/location.enum';
import { Country } from '../../../../model/country.model';
import { ListObject } from '../../../../model/listObject.model';
import { State } from '../../../../model/state.model';

@Component({
  selector: 'app-country-abm',
  templateUrl: './country-abm.component.html'
})
export class CountryAbmComponent implements OnInit, OnDestroy {
  @Input() countrySelected: Country;
  @Input() abmCountry: boolean;

  country: UntypedFormGroup;
  fb: UntypedFormBuilder;
  evt: Country;
  buttonAction: string;

  abmLocation: ListObject;
  addCity = false;
  abmstate = false
 // stateSelected: State|null;

  constructor(fb: UntypedFormBuilder, private service: AppService) {
    this.fb = fb;
    this.abmLocation = new ListObject();
  }

  ngOnDestroy() {
  }

  ngOnInit(): void {

    console.log("country abm init");

    if (this.abmCountry) {
      if (this.countrySelected != undefined) {
        this.country = this.CreateForm(this.countrySelected);
      } else {
        this.abmCountry = false;
      }
    }
  }

  CreateForm(countryEdit: Country | null): UntypedFormGroup {
    if (countryEdit == null) {

      this.buttonAction = "Add";

      return this.fb.group({
        name: [null, [Validators.required]],
        code: [null],
        capital: [null],
        region: [null],
        lat: [null],
        lng:[null]
      });
    } else {
      this.buttonAction = "Update";

      this.addCity = true;

      return this.fb.group({
        name: new UntypedFormControl(countryEdit.name ?? null),
        code: new UntypedFormControl(countryEdit.code ?? null),
        capital: new UntypedFormControl(countryEdit.capital ?? null),
        region: new UntypedFormControl(countryEdit.region ?? null),
        lat: new UntypedFormControl(countryEdit.lat ?? null),
        lng: new UntypedFormControl(countryEdit.lng ?? null)
      });

    }

  }

  ngOnChanges() {
    if (this.countrySelected == null) {
      this.country = this.CreateForm(null);
    } else {
      this.evt = this.countrySelected;
    }
  }

  OpenEdit() {
    this.abmCountry = true;
    this.country = this.CreateForm(null);

  }

  OpenNewCity() {
    if (this.addCity) {
      this.abmstate = true;
    //  this.stateSelected = null;
      this.abmCountry = false;
    }

    //this.abmLocation.rowSelected = this.countrySelected;
    //this.abmLocation.type = LocationEnum.country;
    //this.abmLocation.abmObject = false;
    //this.service.sendAddStateToCountry(this.abmLocation);
  }

  SaveCountry(CountryABM: UntypedFormGroup) {
    var evt = CountryABM.value as Country;
    console.log('Current data: ', CountryABM);

    if (this.buttonAction == "Update") {

      evt.id = (this.countrySelected).id;

      this.service.UpdateCountries((this.countrySelected).id, evt)
        .pipe(first())
        .subscribe(
          data => {
            this.abmLocation.abmObject = false;
            this.service.sendUpdateCountry(this.abmLocation);
          },
          error => console.log('Error Getting Position: ', error)
        );
    }
    else {

      this.service.AddCountries(evt).pipe(first())
        .subscribe(
          data => {
            this.abmLocation.abmObject = false;
            this.service.sendUpdateCountry(this.abmLocation);
          },
          error => console.log('Error Getting Position: ', error)
        );
    }
  }


  ResetForm() {
    this.country = this.CreateForm(null);
  }

  Cancel() {
    this.service.sendUpdateCountry(true);
  }

}


