import { Component, Input, OnInit } from "@angular/core";
import { FormControl } from "@angular/forms";
import { map, startWith, Observable, first } from "rxjs";
import { State } from "../../../../model/state.model";
import { AppService } from "../../../../server/app.service";

@Component({
  selector: 'app-filter-states',
  templateUrl: './filterStates.component.html'
})
export class FilterStatesComponent implements OnInit {

  @Input() dataState: State;

  selectedState: State;

  optionsStates: State[];
  statesControl = new FormControl('');
  statesOptions: Observable<State[]>;

  constructor(private service: AppService) {

  }

  ngOnInit(): void {
    this.GetAllCountries()
  }

  GetAllCountries() {
    console.log('event type list');

    this.service.GetStates('Title', 'desc', 0, 10000, '')
      .pipe(first())
      .subscribe(
        data => {
          console.log('Current data filter event type: ', data);

          this.optionsStates = data;

          this.statesOptions = this.statesControl.valueChanges.pipe(
            startWith(''),
            map(value => {
              const name = typeof value === 'string' ? value : value?.name;
              return name ? this._filterCountry(name as string) : this.optionsStates.slice();
            }),
          );

        },
        error => console.log('Error Getting Position: ', error)
      );
  }

  displayCountries = (user: State): string => {

    if (this.dataState != undefined) {
      if (user.id != undefined) {
        this.dataState = user;
        this.service.sendUpdatePeople(this.dataState);
      } else {
        this.statesControl.setValue(this.dataState);
      }
    }

    return user && user.name ? user.name : '';
  }

  private _filterCountry(name: string): State[] {
    const filterValue = name.toLowerCase();

    return this.optionsStates.filter(option => option.name.toLowerCase().includes(filterValue)
      || option.capital?.toLowerCase().includes(filterValue)
      || option.code?.toLowerCase().includes(filterValue)
      || option.region?.toLowerCase().includes(filterValue));
  }

}
