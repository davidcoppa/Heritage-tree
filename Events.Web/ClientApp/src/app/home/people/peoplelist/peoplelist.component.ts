import { Component, AfterViewInit, ViewChild, OnInit } from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import { Router } from '@angular/router';
import { BehaviorSubject, merge, of } from 'rxjs';
import { startWith, switchMap, catchError, map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Gender } from 'src/app/helpers/enums/gender.enum';
import { Person } from 'src/app/model/person.model';
import { AppService } from 'src/app/server/app.service';

@Component({
  selector: 'app-peoplelist',
  templateUrl: './peoplelist.component.html',
  styleUrls: ['./peoplelist.component.css']
})
export class PeoplelistComponent implements AfterViewInit {
  displayedColumns: string[] = ['FirstName', 
                                'SecondName', 
                                'FirstSurname', 
                                'SecondSurname',
                                'Sex','Order',
                                'DateOfBirth',
                                'PlaceOfBirth',
                                'DateOfDeath',
                                'PlaceOfDeath',
                                'Photos',
                                'Action'];
  person: Person[] = [];
  @ViewChild(MatSort) sort!: MatSort;
  term$ = new BehaviorSubject<string>('');
  resultsLength = 0;
  pageSize = 15;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  abmperson:boolean=false;
  constructor(private appService: AppService, private router: Router) { }
  gender=Gender;
  rowSelected:Person;

  
  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
     this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);
    
     console.log(this.gender);
 
     merge(this.sort.sortChange, this.term$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          return this.appService!.getPeople(this.sort.active, this.sort.direction, this.paginator.pageIndex,this.pageSize,(searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : '')
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

  editContact(contact: Person) {
    // let route = '/contacts/edit-contact';
    // this.router.navigate([route], { queryParams: { id: contact.id } });
    this.abmperson=true;   
    this.rowSelected=contact;

  }

  viewContact(contact: Person) {
    let route = '/contacts/view-contact';
    this.router.navigate([route], { queryParams: { id: contact.Id } });
  }
  viewMedia(contact: Person) {
    let route = '/contacts/view-media';
    this.router.navigate([route], { queryParams: { id: contact.Id } });
  }


}


