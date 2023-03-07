import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { AppService } from 'src/app/server/app.service';
import { LocationEnum } from '../../../../helpers/enums/location.enum';
import { City } from '../../../../model/city.model';
import { Country } from '../../../../model/country.model';
import { ListObject } from '../../../../model/listObject.model';
import { State } from '../../../../model/state.model';

@Component({
  selector: 'app-location-abm',
  templateUrl: './location-abm.component.html'
})
export class LocationAbmComponent implements OnInit, OnDestroy {

  @Input() abmLocation: ListObject;
  @Input() abmObject: boolean;

  abmCountry: boolean;
  abmState: boolean;
  abmCity: boolean;

  countrySelected: Country;
  stateSelected: State;
  citySelected: City;

  constructor(private service: AppService) {

  }

  ngOnDestroy() {
  }

  ngOnInit(): void {

  }

  ngOnChanges() {

    if (this.abmLocation.type != undefined && this.abmLocation.type == LocationEnum.country) {
      console.log("edit country");

      this.abmCountry = true;
      this.abmState = false;
      this.abmCity = false;

      this.countrySelected = this.abmLocation.rowSelected as Country;
    }
    if (this.abmLocation.type != undefined && this.abmLocation.type == LocationEnum.state) {
      console.log("edit state");

      this.abmCountry = false;
      this.abmState = true;
      this.abmCity = false;

      this.stateSelected = this.abmLocation.rowSelected as State;

    }
    if (this.abmLocation.type != undefined && this.abmLocation.type == LocationEnum.city) {
      console.log("edit city");

      this.abmCountry = false;
      this.abmState = false;
      this.abmCity = true;

      this.citySelected = this.abmLocation.rowSelected as City;

    }
  }

}


