import { AfterContentInit, AfterViewInit, Component, Input, OnChanges, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { BehaviorSubject, catchError, debounceTime, distinctUntilChanged, map, of, merge, startWith, switchMap, Subscription } from 'rxjs';
import { Person } from 'src/app/model/person.model';
import { AppService } from 'src/app/server/app.service';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.css']
})
export class PeopleComponent implements OnInit, OnChanges {
  @Input() abmperson: boolean;
  person: Person[] = [];
  @Input() sort!: MatSort;
  @Input() paginator!: MatPaginator;

  private subscriptionName: Subscription; //important to create a subscription


  term$ = new BehaviorSubject<string>('');

  resultsLength = 0;
  pageSize = 15;
  dataPerson: Person[];
  personSelected: Person;

  constructor(private appService: AppService) {
    this.subscriptionName = this.appService.getUpdate().subscribe
      (data => { //message contains the data sent from service
        if (data.data==true) {
          this.abmperson = false;

        }
        if (data.data.abmperson == true) {
          this.abmperson = data.data.abmperson;
          this.personSelected = data.data.rowSelected;
        }
        else {
          this.sort = data.data.sort;
          this.paginator = data.data.paginator;

          this.ngOnChanges();
        }
       
      });
  }

  ngOnInit(): void {
  }


  //todo: hacer objeto que tenga todo lo necesario y pasarlo
  ngOnChanges() {
    console.log("changes");
    if (this.sort == undefined) { return; }

    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);

    // console.log(this.gender);

    merge(this.sort.sortChange, this.term$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          return this.appService!.getPeople(this.sort.active, this.sort.direction, this.paginator.pageIndex, this.pageSize, (searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : '')
            .pipe(catchError(() =>
              of(null)
            ));
        }),
        map(data => {

          console.log(data);

          if (data === null) {
            return [];
          }
          this.resultsLength = data.length;

          return data;
        })
      ).subscribe(data => this.person = data);



  }
  addPeople() {
    this.abmperson = !this.abmperson;

  }


}
