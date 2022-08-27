import { Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
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
export class EventComponent implements OnInit, OnChanges, OnDestroy {


  @Input() abmObject: boolean;
    event: Events[] = [];
  @Input() sort!: MatSort;
  @Input() paginator!: MatPaginator;

  private subscriptionName: Subscription; //important to create a subscription


  termEvent$ = new BehaviorSubject<string>('');

  resultsLength = 0;
  pageSize = 15;
  dataEvent: Events[];
  eventSelected: Events;

  constructor(private service: AppService) {
    this.subscriptionName = this.service.getUpdate().subscribe
      (data => { //message contains the data sent from service
        if (data!= undefined) {
          
       
        if (data.data == true) {
          this.abmObject = false;

        }
        if (data.data.abmObject == true) {
          this.abmObject = data.data.abmObject;
          this.eventSelected = data.data.rowSelected;
        }
        else {
          this.sort = data.data.sort;
          this.paginator = data.data.paginator;

          this.ngOnInit();
        }
        }
      });
  }
  ngOnInit(): void {
    console.log("changes event");
    if (this.sort == undefined) { return; }

    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);

    // console.log(this.gender);

    merge(this.sort.sortChange, this.termEvent$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
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
  ngOnChanges(): void {
    

  }

  ngOnDestroy() {
    this.subscriptionName.unsubscribe();
  }

  addEvent() {
    this.abmObject = !this.abmObject;

  }

}
