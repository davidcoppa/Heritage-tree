import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { first, Subscription } from 'rxjs';
import { AppService } from 'src/app/server/app.service';
import { City } from '../../../../model/city.model';
import { ListObject } from '../../../../model/listObject.model';

@Component({
  selector: 'app-city-abm',
  templateUrl: './city-abm.component.html'
})
export class CityAbmComponent implements OnInit, OnDestroy {
  @Input() citySelected: City;
  @Input() abmCity: boolean;

  city: UntypedFormGroup;
  fb: UntypedFormBuilder;
  evt: City;
  buttonAction: string;

  listModel: ListObject;

  private subscriptionCityFilter: Subscription;

  constructor(fb: UntypedFormBuilder, private service: AppService) {

    this.fb = fb;

    this.subscriptionCityFilter = this.service.getUpdateCity().subscribe
      (data => { //message contains the data sent from service
        console.log("sendUpdate city: " + data.data);
        this.citySelected = data.data;
      });
  }

  ngOnDestroy() {
    this.subscriptionCityFilter.unsubscribe();
  }

  ngOnInit(): void {
    console.log("city edit init: ");

    if (this.citySelected != undefined) {
      this.city = this.CreateForm(this.citySelected);
    } else {
      this.city = this.CreateForm(null);
    }
  }

  CreateForm(cityEdit: City | null): UntypedFormGroup {
    if (cityEdit == null) {

      this.buttonAction = "Add";

      return this.fb.group({
        name: [null, [Validators.required]],
        code: [null],
        capital: [null],
        region: [null],
        coordinates: [null]
      });
    } else {
      this.evt = this.citySelected;
      this.buttonAction = "Update";

      return this.fb.group({
        name: new UntypedFormControl(cityEdit.name ?? null),
        code: new UntypedFormControl(cityEdit.code ?? null),
        capital: new UntypedFormControl(cityEdit.capital ?? null),
        region: new UntypedFormControl(cityEdit.region ?? null),
        coordinates: new UntypedFormControl(cityEdit.coordinates ?? null)        
      });
    }

  }

  ngOnChanges() {
    console.log("city on changes: ");

    if (this.citySelected == null) {
      this.city = this.CreateForm(null);
    } else {
      this.evt = this.citySelected;
    }
  }

  SaveCity(cityABM: UntypedFormGroup) {
    this.evt = cityABM.value as City;


    console.log('Current city abm: ', this.evt);

    if (this.buttonAction == "Update") {

      this.evt.id = (this.citySelected).id;

      this.service.UpdateCity((this.citySelected).id, this.evt)
        .pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMCityFinished();
          },
          error => console.log('Error Getting Position: ', error)
        );
    }
    else {

      this.service.AddCity(this.evt).pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMCityFinished();

          },
          error => console.log('Error Getting Position: ', error)
        );
    }
  }

  ABMCityFinished() {
    this.service.sendUpdateCity(true);
  }

  ResetForm() {
    this.city = this.CreateForm(null);
  }

  Cancel() {
    this.service.sendUpdateCity(true);
  }

}


