import { Component, Input, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { BehaviorSubject, catchError, debounceTime, distinctUntilChanged, map, merge, of, startWith, Subscription, switchMap } from 'rxjs';
import { EventType } from '../../model/eventType.model';
import { AppService } from '../../server/app.service';

@Component({
  selector: 'app-event.type',
  templateUrl: './eventtype.component.html',
  styleUrls: ['./eventtype.component.css']
})
export class EventTypeComponent implements OnInit {
  //addEventTypes

  @Input() abmEventType: boolean;
  eventType: EventType[] = [];
  @Input() sort!: MatSort;
  @Input() paginator!: MatPaginator;

  private subscriptionName: Subscription; //important to create a subscription


  term$ = new BehaviorSubject<string>('');

  resultsLength = 0;
  pageSize = 15;
 // dataEventType: EventType[];
  evenntSelected: EventType;


  constructor(private service: AppService) {
    this.subscriptionName = this.service.getUpdate().subscribe
      (data => { //message contains the data sent from service
        if (data.data == true) {
          this.abmEventType = false;

        }
        if (data.data.abmperson == true) {
          this.abmEventType = data.data.abmperson;
          this.evenntSelected = data.data.rowSelected;
        }
        else {
          this.sort = data.data.sort;
          this.paginator = data.data.paginator;

         this.ngOnChanges();
        }

      }); }

  ngOnInit(): void {
  }
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
          return this.service!.GetEventType(this.sort.active, this.sort.direction, this.paginator.pageIndex, this.pageSize, (searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : '')
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
    ).subscribe(data => this.eventType = data);



  }
  addEventTypes() {
    this.abmEventType = !this.abmEventType;

  }
}
