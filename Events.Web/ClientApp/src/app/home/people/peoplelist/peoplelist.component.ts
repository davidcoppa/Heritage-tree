import { Component, AfterViewInit, ViewChild, OnInit, Input, Output } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { Person } from 'src/app/model/person.model';
import { AppService } from 'src/app/server/app.service';
import { Gender } from '../../../helpers/enums/gender.enum';
import { ListObject } from '../../../model/listObject.model';

@Component({
  selector: 'app-peoplelist',
  templateUrl: './peoplelist.component.html',
  styleUrls: ['./peoplelist.component.css']
})
export class PeoplelistComponent {//implements AfterViewInit {
  displayedColumns: string[] = ['FirstName',
    'SecondName',
    'FirstSurname',
    'SecondSurname',
    'Sex', 'Order',
    'Father',
    'Mother',
    'DateOfBirth',
    'PlaceOfBirth',
    'DateOfDeath',
    'PlaceOfDeath',
    'Photos',
    'Action'];

  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  @Input() dataPerson: Person[];
  resultsLength = 0;
  listModel: ListObject;

  gender = Gender;

  itemToVisualize = false;
  personToShow: Person;

  constructor(private router: Router, private service: AppService) {

  }

  ngAfterViewInit() {
    console.log("ctor people list");

    this.listModel = new ListObject();
    this.sort.direction = "desc";
    this.sort.active = "FirstName";
    this.sort.disableClear;
    this.paginator = this.paginator;

    this.listModel.sort = this.sort;
    this.listModel.paginator = this.paginator;

    this.service.sendUpdateObject(this.listModel);
  }



  editContact(contact: Person) {
    this.listModel.abmObject = true;
    this.listModel.rowSelected = contact;

    this.service.sendUpdateObject(this.listModel);
  }

  viewContact(contact: Person) {
    this.itemToVisualize = true;
    this.personToShow = contact;
  }

  reset() {
    this.itemToVisualize = false;
  }
  viewMedia(contact: Person) {
    //let route = '/contacts/view-media';
    //this.router.navigate([route], { queryParams: { id: contact.id } });
  }


}


