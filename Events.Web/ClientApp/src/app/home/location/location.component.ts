import { ChangeDetectorRef, Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BehaviorSubject, catchError, debounceTime, distinctUntilChanged, map, of, merge, startWith, switchMap, Subscription } from 'rxjs';
import { LocationEnum } from '../../helpers/enums/location.enum';
import { City } from '../../model/city.model';
import { Country } from '../../model/country.model';
import { State } from '../../model/state.model';
import { AppService } from '../../server/app.service';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html'
})
export class LocationComponent implements OnChanges, OnDestroy {


  @Input() abmObject: boolean;
  @Input() abmCountry: boolean;
  @Input() abmState: boolean;
  @Input() abmCity: boolean;

  countries: Country[] = [];
  states: State[] = [];
  @Input() sort!: MatSort;
  @Input() paginator!: MatPaginator;

  private subscriptionCountry: Subscription;
  private subscriptionState: Subscription;
  private subscriptionCity: Subscription;

  termEvent$ = new BehaviorSubject<string>('');

  resultsLength = 0;
  pageSize = 15;
  dataCountry: Country[];
  countrySelected: Country;
  stateSelected: State;
  citySelected: City;

  expandedElement: Country | null;
  expandedElementCity: State | null;


  constructor(private service: AppService,
    private cd: ChangeDetectorRef
  ) {

    this.subscriptionCountry = this.service.getUpdateCountry().subscribe
      (data => { 
        if (data != undefined) {

          if (data.data.abmObject == true) {
            this.abmObject = data.data.abmObject;
            this.countrySelected = data.data.rowSelected;

            if (data.data.type != undefined && data.data.type == LocationEnum.country) {
              this.abmCountry = true;
              this.abmState = false;
              this.abmCity = false;
            }
          }
          else {
            this.sort = data.data.sort;
            this.paginator = data.data.paginator;
            this.LoadData();
          }
        }
      });

    this.subscriptionState = this.service.getUpdateState().subscribe
      (data => { 
        if (data != undefined) {

          if (data.data.abmObject == true) {
            this.abmObject = data.data.abmObject;
            this.stateSelected = data.data.rowSelected;

            if (data.data.type != undefined && data.data.type == LocationEnum.state) {

              this.abmCountry = false;
              this.abmState = true;
              this.abmCity = false;
            }
          }
        }
      });


    this.subscriptionCity = this.service.getUpdateCity().subscribe
      (data => { 
        if (data != undefined) {

          if (data.data.abmObject == true) {
            this.abmObject = data.data.abmObject;
            this.citySelected = data.data.rowSelected;

            if (data.data.type != undefined && data.data.type == LocationEnum.city) {
              this.abmCountry = false;
              this.abmState = false;
              this.abmCity = true;
            }
          }
        }
      });
  }

  LoadData(): void {
    if (this.sort == undefined) { return; }

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);

    merge(this.sort.sortChange, this.termEvent$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          return this.service!.GetCountries(this.sort.active, this.sort.direction, this.paginator.pageIndex, this.pageSize, (searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : '')
            .pipe(catchError(() =>
              of(null)
            ));
        }),
        map(data => {

          if (data === null) {
            return [];
          }
          this.resultsLength = data.length;

          return data;
        })
      ).subscribe(data => {
        console.log(data);
        this.countries = [];
        this.states = [];

        data.forEach(user => {
          if (user.state && Array.isArray(user.state) && user.state.length) {

            var userState = user.state;

            user.state.forEach(stateData => {

              if (stateData.cities && Array.isArray(stateData.cities) && stateData.cities.length) {

                this.states = [...this.states, { ...stateData, cities: new MatTableDataSource(stateData.cities) }];

                this.countries = [...this.countries, { ...user, state: new MatTableDataSource(this.states) }];
              }
              else {
                this.countries = [...this.countries, { ...user, state: new MatTableDataSource(userState) }];
              }
            })
          } else {
            this.countries = [...this.countries, user];
          }

        });
      });

  }

  ngOnChanges(): void {
  }

  ngOnDestroy() {
    this.subscriptionCountry.unsubscribe();
    this.subscriptionState.unsubscribe();
    this.subscriptionCity.unsubscribe();
  }

  addEvent() {
    this.abmObject = !this.abmObject;
  }
}
