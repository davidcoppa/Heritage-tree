import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { Events } from 'src/app/model/event.model';
import { AppService } from 'src/app/server/app.service';
import { ListObject } from '../../../model/listObject.model';

@Component({
  selector: 'app-eventlist',
  templateUrl: './eventlist.component.html',
  styleUrls: ['./eventlist.component.css']
})
export class EventlistComponent implements AfterViewInit {
  displayedColumns: string[] = ['Title',
    'EventDate',
    'EventType',
    'Person1',
    'Person2',
    'Person3',
    'Location',
    'Photos',
    'Action'  ];


  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  @Input() dataEvent: Events[];
  resultsLength = 0;
  listModel: ListObject;

  constructor(private service: AppService, private router: Router) { }

  ngAfterViewInit() {


    console.log("ctor event list");

    this.listModel = new ListObject();
    this.sort.direction = "desc";
    this.sort.active = "Title";
    this.sort.disableClear;
    this.paginator = this.paginator;

    this.listModel.sort = this.sort;
    this.listModel.paginator = this.paginator;

    this.service.sendUpdateObject(this.listModel);
  }

  editEvent(contact: Events) {
    this.listModel.abmObject = true;
    this.listModel.rowSelected = contact;

    this.service.sendUpdateObject(this.listModel);


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
