import { Component, AfterViewInit, ViewChild } from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import { Router } from '@angular/router';
import { BehaviorSubject, merge, of } from 'rxjs';
import { startWith, switchMap, catchError, map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Person } from 'src/app/model/person.model';
import { AppService } from 'src/app/server/app.service';

@Component({
  selector: 'app-peoplelist',
  templateUrl: './peoplelist.component.html',
  styleUrls: ['./peoplelist.component.css']
})
export class PeoplelistComponent implements AfterViewInit {
  displayedColumns: string[] = ['first name', 'second name', 'first surname', 'second surname','sex','order','date of birth','place of birth','date of death','place of death','media'];
  data: Person[] = [];
  @ViewChild(MatSort) sort!: MatSort;
  term$ = new BehaviorSubject<string>('');
  resultsLength = 0;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
 
  constructor(private appService: AppService, private router: Router) { }
 
  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
 
    merge(this.sort.sortChange, this.term$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          return this.appService!.getPeople(this.sort.active, this.sort.direction, this.paginator.pageIndex,(searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : 'repo:angular/components')
            .pipe(catchError(() => of(null)));
        }),
        map(data => {
          if (data === null) {
            return [];
          }
          this.resultsLength = data.totalItems;

          return data.person;
        })
      ).subscribe(data => this.data = data);
  }

  editContact(contact: Person) {
    let route = '/contacts/edit-contact';
    this.router.navigate([route], { queryParams: { id: contact.id } });
  }

  viewContact(contact: Person) {
    let route = '/contacts/view-contact';
    this.router.navigate([route], { queryParams: { id: contact.id } });
  }
  viewMedia(contact: Person) {
    let route = '/contacts/view-media';
    this.router.navigate([route], { queryParams: { id: contact.id } });
  }


}




// export interface GithubApi {
//   items: GithubIssue[];
//   total_count: number;
// }
 
// export interface GithubIssue {
//   created_at: string;
//   number: string;
//   state: string;
//   title: string;
// }
 