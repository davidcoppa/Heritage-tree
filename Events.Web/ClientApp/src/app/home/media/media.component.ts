import { Component, Input, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BehaviorSubject, catchError, debounceTime, distinctUntilChanged, map, merge, of, startWith, Subscription, switchMap } from 'rxjs';
import { FileData } from '../../model/fileData.model';
import { Media } from '../../model/media.model';
import { AppMediaService } from '../../server/app.media.service';

@Component({
  selector: 'app-media',
  templateUrl: './media.component.html',
  styleUrls: ['./media.component.css']
})
export class MediaComponent implements OnInit {

  @Input() abmObject: boolean;
  media: Media[] = [];
  file: FileData[]=[];
  @Input() sort!: MatSort;
  @Input() paginator!: MatPaginator;

  private subscriptionMedia: Subscription; //important to create a subscription


  termMedia$ = new BehaviorSubject<string>('');

  resultsLength = 0;
  pageSize = 15;
  dataMedia: Media[];
  mediaSelected: Media;

  constructor(private appMediaService: AppMediaService) {
    this.subscriptionMedia = this.appMediaService.getUpdateMedia().subscribe
      (data => { //message contains the data sent from service
        if (data.data == true) {
          this.abmObject = false;

        }
        if (data.data.abmObject == true) {
          this.abmObject = data.data.abmObject;
          this.mediaSelected = data.data.rowSelected;
        }
        else {
          this.sort = data.data.sort;
          this.paginator = data.data.paginator;

          this.ngOnInit();
        }

      });
  }

  ngOnInit(): void {
    console.log("init media");
    if (this.sort == undefined) { return; }

    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 1);

    // console.log(this.gender);

    merge(this.sort.sortChange, this.termMedia$.pipe(debounceTime(1000), distinctUntilChanged()), this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          return this.appMediaService!.getMedia(this.sort.active, this.sort.direction, this.paginator.pageIndex, this.pageSize, (searchTerm && typeof searchTerm == 'string') ? searchTerm.toString() : '')
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
      //).subscribe(data => this.media = data);
    ).subscribe(data => {
      console.log(data);

      this.media = [];
      this.file = [];

      data.forEach(user => {
        if (user.file && Array.isArray(user.file) && user.file.length) {

          var userState = user.file;

          this.media = [...this.media, { ...user, file: new MatTableDataSource(userState) }];

        } else {
          this.media = [...this.media, user];
        }

      });


    });


  }


  //todo: hacer objeto que tenga todo lo necesario y pasarlo
  ngOnChanges() {


  }
  addMedia() {
    this.abmObject = !this.abmObject;

  }
  ngOnDestroy() {
    this.subscriptionMedia.unsubscribe();
  }

}
