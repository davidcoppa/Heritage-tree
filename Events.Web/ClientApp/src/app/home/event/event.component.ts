import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { BehaviorSubject, catchError, debounceTime, distinctUntilChanged, map, of, merge, startWith, switchMap, Subscription } from 'rxjs';
import { Events } from '../../model/event.model';
import { AppService } from '../../server/app.service';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnChanges {


  @Input() abmEvent: boolean;
    event: Events[] = [];
  @Input() sort!: MatSort;
  @Input() paginator!: MatPaginator;

  private subscriptionName: Subscription; //important to create a subscription


  term$ = new BehaviorSubject<string>('');

  resultsLength = 0;
  pageSize = 15;
  dataEvent: Events[];
  eventSelected: Events;
    appService: any;

  constructor(private service: AppService) {
    this.subscriptionName = this.service.getUpdate().subscribe
      (data => { //message contains the data sent from service
        if (data.data == true) {
          this.abmEvent = false;

        }
        if (data.data.abmperson == true) {
          this.abmEvent = data.data.abmperson;
          this.eventSelected = data.data.rowSelected;
        }
        else {
          this.sort = data.data.sort;
          this.paginator = data.data.paginator;

       this.ngOnChanges();
        }

      });
  }

  ngOnChanges(): void {
    console.log("changes");
    if (this.sort == undefined) { return; }

    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);

    // console.log(this.gender);

    merge(this.sort.sortChange, this.term$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          return this.service!.getEvents(this.sort.active, this.sort.direction, this.paginator.pageIndex, this.pageSize, (searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : '')
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
      ).subscribe(data => this.event = data);


  }

  addEvent() {
    this.abmEvent = !this.abmEvent;

  }

}
