import { AfterViewInit, Component, Input, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Media } from 'src/app/model/media.model';
import { AppMediaService } from '../../../server/app.media.service';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { ListObject } from '../../../model/listObject.model';
import { ListSubObject } from '../../../model/listSubObject';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { FileData } from '../../../model/fileData.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-media-list',
  templateUrl: './media-list.component.html',
  styleUrls: ['./media-list.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ]
})
export class MediaListComponent implements AfterViewInit {

  displayedColumns: string[] = ['Name',
    'Description',
    'Date',
    'Tags',
    'File',
    'Action'
  ];

  mediaDisplayedColumns: string[] = ['Name',
    'Description',
    'DateUploaded',
    'DocumentType',
    'Url',
    'Img',
    'Action'];



  @Input() dataMedia: Media[];
  @Input() abmMedia: boolean;

  // media: Media[] = [];
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  @ViewChildren('innerTableMedia') innerTableMedia: QueryList<MatTable<Object>>;
  @ViewChildren('innerSortMedia') innerSortMedia: QueryList<MatSort>;

  resultsLength = 0;
  listModel: ListObject;
  listSubObject: ListSubObject;

  expandedElementMedia: Media | null;

  rowSelected: Media;


  constructor(private appMediaService: AppMediaService, private router: Router) { }

  ngAfterViewInit() {
    console.log("ctor media list");

    this.listModel = new ListObject();
    this.sort.direction = "desc";
    this.sort.active = "Name";
    this.sort.disableClear;
    this.paginator = this.paginator;

    this.listModel.sort = this.sort;
    this.listModel.paginator = this.paginator;

    this.appMediaService.sendUpdateMedia(this.listModel);
  }

  toggleRowMedia(element: object) {
    console.log("media list click - element: " + element);

    var elementCity = element as Media;
    //unknown
    (elementCity.file != [] && (elementCity.file as MatTableDataSource<FileData>).data?.length) ?
      (this.expandedElementMedia = this.expandedElementMedia === elementCity ? null : elementCity) : null;





  }

  applyFilter(filterValue: string) {
    this.innerTableMedia.forEach((table, index) => (table.dataSource as MatTableDataSource<Media>).filter = filterValue.trim().toLowerCase());
  }

  editMedia(contact: Media) {
    this.abmMedia = true;

    this.listModel.abmObject = true;
    this.listModel.rowSelected = contact;

    this.appMediaService.sendUpdateMedia(this.listModel);

  }

  viewMedia(contact: Media) {
    let route = '/media/view-media';
    this.router.navigate([route], { queryParams: { id: contact.id } });
  }

}
