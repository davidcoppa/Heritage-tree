import { Component, Input, OnInit } from "@angular/core";
import { UntypedFormControl } from "@angular/forms";
import { map, startWith, Observable, first } from "rxjs";
import { City } from "../../../../model/city.model";
import { AppService } from "../../../../server/app.service";

@Component({
  selector: 'app-filter-city',
  templateUrl: './filterCity.component.html'
})
export class FilterCityComponent implements OnInit {

  @Input() dataCity: City;

  selectedCity: City;

  optionsCities: City[];
  citiesControl = new UntypedFormControl('');
  citiesOptions: Observable<City[]>;

  constructor(private service: AppService) {

  }

  ngOnInit(): void {
    this.GetAllCountries()
  }

  GetAllCountries() {
    console.log('event type list');

    this.service.GetCities('Name', 'desc', 0, 10000, '')
      .pipe(first())
      .subscribe(
        data => {
          console.log('Current data filter event type: ', data);

          this.optionsCities = data;

          this.citiesOptions = this.citiesControl.valueChanges.pipe(
            startWith(''),
            map(value => {
              const name = typeof value === 'string' ? value : value?.name;
              return name ? this._filterCountry(name as string) : this.optionsCities.slice();
            }),
          );

        },
        error => console.log('Error Getting Position: ', error)
      );
  }

  displayCity = (user: City): string => {

    if (this.dataCity != undefined) {
      if (user.id != undefined) {
        this.dataCity = user;
        this.service.sendUpdatePeople(this.dataCity);
      } else {
        this.citiesControl.setValue(this.dataCity);
      }
    }

    return user && user.name ? user.name : '';
  }

  private _filterCountry(name: string): City[] {
    const filterValue = name.toLowerCase();

    return this.optionsCities.filter(option => option.name.toLowerCase().includes(filterValue)
      || option.capital?.toLowerCase().includes(filterValue)
      || option.code?.toLowerCase().includes(filterValue)
      || option.region?.toLowerCase().includes(filterValue));
  }

}
