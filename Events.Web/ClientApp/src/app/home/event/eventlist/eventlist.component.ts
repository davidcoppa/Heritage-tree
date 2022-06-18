import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, debounceTime, distinctUntilChanged, map, merge, of, startWith, switchMap } from 'rxjs';
import { Events } from 'src/app/model/event.model';
import { AppService } from 'src/app/server/app.service';

@Component({
  selector: 'app-eventlist',
  templateUrl: './eventlist.component.html',
  styleUrls: ['./eventlist.component.css']
})
export class EventlistComponent implements AfterViewInit {
  displayedColumns: string[] = ['Title',
                                 'Description',
                                 'EventDate',
                                 'EventType',
                                 'Person1',
                                 'Person2',
                                 'Person3',
                                 'Location',
                                 'Photos'];

  event: Events[] = [];
  @ViewChild(MatSort) sort!: MatSort;
  term$ = new BehaviorSubject<string>('');
  resultsLength = 0;
  pageSize = 15;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  abmEvent:boolean=false;
  constructor(private appService: AppService, private router: Router) { }
 
  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
     this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);
 
     merge(this.sort.sortChange, this.term$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          return this.appService!.getEvents(this.sort.active, this.sort.direction, this.paginator.pageIndex,this.pageSize,(searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : '')
            .pipe(catchError(() => of(null)));
        }),
        map(data => {
          if (data === null) {
            return [];
          }
          this.resultsLength = data.length;

          return data;
        })
      ).subscribe(data => this.event = data);
  }

  editEvent(contact: Events) {
    // let route = '/contacts/edit-contact';
    // this.router.navigate([route], { queryParams: { id: contact.id } });
    this.abmEvent=true;   

  }

  viewEvent(contact: Events) {
    // let route = '/contacts/view-contact';
    // this.router.navigate([route], { queryParams: { id: contact.id } });
  }
  viewEventMedia(contact: Events) {
    // let route = '/contacts/view-media';
    // this.router.navigate([route], { queryParams: { id: contact.id } });
  }

}
