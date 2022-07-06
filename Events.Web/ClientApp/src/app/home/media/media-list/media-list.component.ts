import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { BehaviorSubject,of, merge } from 'rxjs';
import { Photos } from 'src/app/model/photos.model';
import { AppService } from 'src/app/server/app.service';
import { startWith, switchMap, catchError, map, debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-media-list',
  templateUrl: './media-list.component.html',
  styleUrls: ['./media-list.component.css']
})
export class MediaListComponent implements AfterViewInit {

  displayedColumns: string[] = ['Name',
                                'Description',
                                'Date',
                                'UrlFile'];
  media: Photos[] = [];
  @ViewChild(MatSort) sort!: MatSort;
  term$ = new BehaviorSubject<string>('');
  resultsLength = 0;
  pageSize = 15;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  abmMedia: boolean = false;
  constructor(private appService: AppService, private router: Router) { }
  rowSelected: Photos;


  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);


    merge(this.sort.sortChange, this.term$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          return this.appService!.getPhotos(this.sort.active, this.sort.direction, this.paginator.pageIndex, this.pageSize, (searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : '')
            .pipe(catchError(() =>
              of(null)
            ));
        }),
        map(data => {

        //  console.log(data);

          if (data === null) {
            return [];
          }
          this.resultsLength = data.length;

          return data;
        })
      ).subscribe(data => this.media = data);
  }

  editMedia(contact: Photos) {
    this.abmMedia = true;
    this.rowSelected = contact;

  }


  viewMedia(contact: Photos) {
    let route = '/media/view-media';
    this.router.navigate([route], { queryParams: { id: contact.Id } });
  }

}
