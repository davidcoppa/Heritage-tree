import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
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

  country: UntypedFormGroup;
  fb: UntypedFormBuilder;
  evt: Country;
  buttonAction: string;

  listModel: ListObject;

  private subscriptionABMLocation: Subscription;

  constructor(fb: UntypedFormBuilder, private service: AppService) {
    //console.log("country abm ctor");
    this.fb = fb;

    this.subscriptionABMLocation = this.service.getUpdateABMLocation().subscribe
      (data => {
 //       console.log("country abm ctor -- data: "+data);
        if (data != undefined) {

          if (data.data.abmObject == true) {
            this.countrySelected = data.data.rowSelected;

            if (data.data.type != undefined && data.data.type == LocationEnum.country) {
              this.abmCountry = true;
            }
          }
        }
      });
  }

  ngOnDestroy() {
    this.subscriptionABMLocation.unsubscribe();
  }

  ngOnInit(): void {
    if (this.countrySelected != undefined) {
      this.country = this.CreateForm(this.countrySelected);
    } else {
      this.country = this.CreateForm(null);
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
        coordinates: [null]
      });
    } else {
      this.evt = this.countrySelected;
      this.buttonAction = "Update";

      return this.fb.group({
        name: new UntypedFormControl(countryEdit.name ?? null),
        code: new UntypedFormControl(countryEdit.code ?? null),
        capital: new UntypedFormControl(countryEdit.capital ?? null),
        region: new UntypedFormControl(countryEdit.region ?? null),
        coordinates: new UntypedFormControl(countryEdit.coordinates ?? null)
      });
    }

  }

  ngOnChanges() {
    //console.log("country on changes: ");

    if (this.countrySelected == null) {
      this.country = this.CreateForm(null);
    } else {
      this.evt = this.countrySelected;
    }
  }

  SaveCountry(CountryABM: UntypedFormGroup) {
    this.evt = CountryABM.value as Country;
    console.log('Current data: ', CountryABM);

    if (this.buttonAction == "Update") {

      this.evt.id = (this.countrySelected).id;

      this.service.UpdateCountries((this.countrySelected).id, this.evt)
        .pipe(first())
        .subscribe(
          data => {
            this.ABMCountryFinished();
          },
          error => console.log('Error Getting Position: ', error)
        );
    }
    else {

      this.service.AddCountries(this.evt).pipe(first())
        .subscribe(
          data => {
       //     console.log('Current data: ', data);
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


