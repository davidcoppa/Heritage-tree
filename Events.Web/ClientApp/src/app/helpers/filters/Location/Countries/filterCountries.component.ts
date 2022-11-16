import { Component, Input, OnInit } from "@angular/core";
import { UntypedFormControl } from "@angular/forms";
import { map, startWith, Observable, first } from "rxjs";
import { Country } from "../../../../model/country.model";
import { AppService } from "../../../../server/app.service";

@Component({
  selector: 'app-filter-countries',
  templateUrl: './filterCountries.component.html'
})
export class FilterCountriesComponent implements OnInit {

  @Input() dataCountry: Country;

  selectedCountry: Country;

  optionsCountries: Country[];
  countriesControl = new UntypedFormControl('');
  CountriesOptions: Observable<Country[]>;

  constructor(private service: AppService) {

  }

  ngOnInit(): void {
    this.GetAllCountries()
  }

  GetAllCountries() {
    console.log('event type list');

    this.service.GetCountries('Name', 'desc', 0, 10000, '')
      .pipe(first())
      .subscribe(
        data => {
          console.log('Current data filter event type: ', data);

          this.optionsCountries = data;

          this.CountriesOptions = this.countriesControl.valueChanges.pipe(
            startWith(''),
            map(value => {
              const name = typeof value === 'string' ? value : value?.name;
              return name ? this._filterCountry(name as string) : this.optionsCountries.slice();
            }),
          );

        },
        error => console.log('Error Getting Position: ', error)
      );
  }

  displayCountries = (user: Country): string => {

    if (this.dataCountry != undefined) {
      if (user.id != undefined) {
        this.dataCountry = user;
        this.service.sendUpdatePeople(this.dataCountry);
      } else {
        this.countriesControl.setValue(this.dataCountry);
      }
    }

    return user && user.name ? user.name : '';
  }

  private _filterCountry(name: string): Country[] {
    const filterValue = name.toLowerCase();

    return this.optionsCountries.filter(option => option.name.toLowerCase().includes(filterValue)
      || option.capital?.toLowerCase().includes(filterValue)
      || option.code?.toLowerCase().includes(filterValue)
      || option.region?.toLowerCase().includes(filterValue));
  }

}
