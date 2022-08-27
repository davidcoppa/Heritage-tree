import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { first } from 'rxjs';
import { EventType } from 'src/app/model/eventType.model';
import { AppService } from 'src/app/server/app.service';
import { ListObject } from '../../../model/listObject.model';

@Component({
  selector: 'app-eventtypelist',
  templateUrl: './eventtypelist.component.html',
  styleUrls: ['./eventtypelist.component.css']
})
export class EventtypelistComponent implements AfterViewInit {
  displayedColumns: string[] = ['Name',
    'Description'];

  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  @Input() dataPerson: EventType[];
  resultsLength = 0;
  listModel: ListObject;

  constructor(private router: Router, private service: AppService,) {

  }

  ngAfterViewInit() {
    console.log("ctor Event Type list");

    this.listModel = new ListObject();
    this.sort.direction = "desc";
    this.sort.active = "Name";
    this.sort.disableClear;
    this.paginator = this.paginator;

    this.listModel.sort = this.sort;
    this.listModel.paginator = this.paginator;

    this.service.sendUpdateObject(this.listModel);
  }


  editEvent(contact: EventType) {
    this.listModel.abmObject = true;
    this.listModel.rowSelected = contact;

    this.service.sendUpdateObject(this.listModel);
  }

}
