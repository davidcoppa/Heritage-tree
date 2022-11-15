import { Component, Input, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { BehaviorSubject, catchError, debounceTime, distinctUntilChanged, map, merge, of, startWith, Subscription, switchMap } from 'rxjs';
import { Media } from '../../model/media.model';
import { AppMediaService } from '../../server/app.media.service';

@Component({
  selector: 'app-media',
  templateUrl: './media.component.html',
  styleUrls: ['./media.component.css']
})
export class MediaComponent implements OnInit {

  @Input() abmObject: boolean;
  termMedia$ = new BehaviorSubject<string>('');
  media: Media[] = [];
  @Input() sort!: MatSort;
  @Input() paginator!: MatPaginator;

  private subscriptionMedia: Subscription;

  resultsLength = 0;
  pageSize = 15;
  dataMedia: Media[];
  mediaSelected: Media;


  constructor(private appServiceMedia: AppMediaService) {
    this.subscriptionMedia = this.appServiceMedia.getUpdateMedia().subscribe
      (data => {
        if (data.data == true) {
          this.abmObject = false;

        }
        if (data.data.abmObject == true) {
          //this.abmObject = data.data.abmObject;
          //this.personSelected = data.data.rowSelected;
        }
        else {
          this.sort = data.data.sort;
          this.paginator = data.data.paginator;

          this.ngOnInit();
        }
      });


  }

  ngOnInit(): void {
    console.log("changes media");


    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);

    // console.log(this.gender);

    merge(this.sort.sortChange, this.termMedia$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          return this.appServiceMedia!.getMedia(this.sort.active, this.sort.direction, this.paginator.pageIndex, this.pageSize, (searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : '')
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
    ).subscribe(data => this.media = data);



  }



  addMedia() {
    this.abmObject = !this.abmObject;

  }
  ngOnDestroy() {
    this.subscriptionMedia.unsubscribe();
  }
}
