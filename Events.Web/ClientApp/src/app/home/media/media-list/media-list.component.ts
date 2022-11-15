import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { BehaviorSubject, of, merge } from 'rxjs';
import { Media } from 'src/app/model/media.model';
import { AppService } from 'src/app/server/app.service';
import { startWith, switchMap, catchError, map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { AppMediaService } from '../../../server/app.media.service';
import { ListObject } from '../../../model/listObject.model';

@Component({
  selector: 'app-media-list',
  templateUrl: './media-list.component.html',
  styleUrls: ['./media-list.component.css']
})
export class MediaListComponent implements AfterViewInit {

  displayedColumns: string[] = [
    'Name',
    'Description',
    'Date',
    'UrlFile',
    'MediaType',
    'Tags'
  ];
  listModel: ListObject;

  media: Media[] = [];
  @ViewChild(MatSort) sort!: MatSort;
  term$ = new BehaviorSubject<string>('');
  resultsLength = 0;
  pageSize = 15;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  abmMedia: boolean = false;
  rowSelected: Media;


  constructor(private router: Router,
    private appServiceMedia: AppMediaService  )
  { }


  ngAfterViewInit() {

    console.log("ctor Media list");

    this.listModel = new ListObject();
    this.sort.direction = "desc";
    this.sort.active = "FirstName";
    this.sort.disableClear;
    this.paginator = this.paginator;

    this.listModel.sort = this.sort;
    this.listModel.paginator = this.paginator;

    this.appServiceMedia.sendUpdateMedia(this.listModel);

  }

  editMedia(contact: Media) {
    this.abmMedia = true;
    this.rowSelected = contact;

    this.appServiceMedia.sendUpdateMedia(this.listModel);

  }


  viewMedia(contact: Media) {
    this.abmMedia = true;
    this.rowSelected = contact;
  }

}
