import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { BehaviorSubject,of, merge } from 'rxjs';
import { Media } from 'src/app/model/media.model';
import { startWith, switchMap, catchError, map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { AppMediaService } from '../../../server/app.media.service';

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
  @Input() dataMedia: Media[];
  media: Media[] = [];
  @ViewChild(MatSort) sort!: MatSort;
  term$ = new BehaviorSubject<string>('');
  resultsLength = 0;
  pageSize = 15;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  abmMedia: boolean = false;
  constructor(private appMediaService: AppMediaService, private router: Router) { }
  rowSelected: Media;


  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);


    merge(this.sort.sortChange, this.term$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          return this.appMediaService!.getMedia(this.sort.active, this.sort.direction, this.paginator.pageIndex, this.pageSize, (searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : '')
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

  editMedia(contact: Media) {
    this.abmMedia = true;
    this.rowSelected = contact;

  }


  viewMedia(contact: Media) {
    let route = '/media/view-media';
    this.router.navigate([route], { queryParams: { id: contact.id } });
  }

}
