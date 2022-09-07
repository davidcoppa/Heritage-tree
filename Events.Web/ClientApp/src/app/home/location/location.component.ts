import { ChangeDetectorRef, Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BehaviorSubject, catchError, debounceTime, distinctUntilChanged, map, of, merge, startWith, switchMap, Subscription } from 'rxjs';
import { Country } from '../../model/country.model';
import { State } from '../../model/state.model';
import { AppService } from '../../server/app.service';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html'
})
export class LocationComponent implements OnChanges, OnDestroy {


  @Input() abmObject: boolean;
  countries: Country[] = [];
  states: State[] = [];
  @Input() sort!: MatSort;
  @Input() paginator!: MatPaginator;

  private subscriptionName: Subscription; //important to create a subscription

  termEvent$ = new BehaviorSubject<string>('');

  resultsLength = 0;
  pageSize = 15;
  dataCountry: Country[];
  countrySelected: Country;

  expandedElement: Country | null;
  expandedElementCity: State | null;
  ;

  constructor(private service: AppService,
    private cd: ChangeDetectorRef
  ) {
    this.subscriptionName = this.service.getUpdateCountry().subscribe
      (data => { //message contains the data sent from service
        if (data != undefined) {


          if (data.data == true) {
            this.abmObject = false;

          }
          if (data.data.abmObject == true) {
            this.abmObject = data.data.abmObject;
            this.countrySelected = data.data.rowSelected;
          }
          else {

            this.sort = data.data.sort;
            this.paginator = data.data.paginator;

            this.LoadData();

          }
        }
      });
 
  }

  LoadData(): void {
    console.log("changes event");
    if (this.sort == undefined) { return; }

    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);

    // console.log(this.gender);


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
    this.subscriptionName.unsubscribe();
  }

  addEvent() {
    this.abmObject = !this.abmObject;

  }

}
