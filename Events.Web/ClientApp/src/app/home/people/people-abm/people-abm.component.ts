import { Component, Input, OnChanges, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { first, Subscription } from 'rxjs';
import { Person } from 'src/app/model/person.model';
import { AppService } from 'src/app/server/app.service';
import { Gender } from 'src/app/helpers/enums/gender.enum';
import { DateAdapter } from '@angular/material/core';
import { CustomDateAdapterService } from '../../../helpers/dates/CustomDateAdapterService';
import { ListObject } from '../../../model/listObject.model';
import { PersonEnum } from '../../../helpers/enums/person.enum';
import { displayPeople } from '../../../model/displayPeople.model';

@Component({
  selector: 'app-people-abm',
  templateUrl: './people-abm.component.html',
  styleUrls: ['./people-abm.component.css'],

})
export class PeopleABMComponent implements OnInit, OnChanges {
  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;
  @Input() personSelected: Person;
  @Input() abmperson: boolean;

  people: FormGroup;
  fb: FormBuilder;

  genderList = Gender;
  person: Person;
  buttonAction: string = "Add";
  listModel: ListObject;

  personSelectedData2: displayPeople;
  personSelectedData3: displayPeople;

  private subscriptionPeopleFilter: Subscription;




  constructor(fb: FormBuilder, private service: AppService, private dateAdapter: DateAdapter<Date>, private dataSer: CustomDateAdapterService) {
    this.dateAdapter.setLocale('en-GB'); //dd/MM/yyyy

    this.fb = fb;

    this.subscriptionPeopleFilter = this.service.getUpdatePeople().subscribe
      (data => { //message contains the data sent from service
        console.log("sendUpdatePeople: " + data.data);
        switch (data.data.personEnum) {

          case PersonEnum.father: {

            this.personSelectedData2 = data.data;
            break;
          }
          case PersonEnum.mother: {

            this.personSelectedData3 = data.data;
            break;
          }
          default: { }
        }



      });
  }

  ngOnInit(): void {

  }

  CreateForm(personEdit: Person | null): FormGroup {

    this.personSelectedData2 = new displayPeople();
    this.personSelectedData3 = new displayPeople();

    this.personSelectedData2.personEnum = PersonEnum.father;
    this.personSelectedData3.personEnum = PersonEnum.mother;
    if (personEdit == null) {

      return this.fb.group({
        firstName: [null, [Validators.required]],
        secondName: [null],
        firstSurname: [null],
        secondSurname: [null],
        sex: [null],
        order: [null],
        dateOfBirth: [null],
        placeOfBirth: [null],
        dateOfDeath: [null],
        placeOfDeath: [null]
        

      });
    }
    else {
      this.person = this.personSelected;
      this.buttonAction = "Update";

      this.personSelectedData2.personSelected = this.personSelected.father;
      this.personSelectedData3.personSelected = this.personSelected.mother;

      return this.fb.group({
        id: new FormControl(this.personSelected.id),
        firstName: new FormControl(personEdit.firstName ?? null),
        secondName: new FormControl(personEdit.secondName ?? null),
        firstSurname: new FormControl(personEdit.firstSurname ?? null),
        secondSurname: new FormControl(personEdit.secondSurname ?? null),
        sex: new FormControl(personEdit.sex ?? null),
        order: new FormControl(personEdit.order ?? null),
        dateOfBirth: new FormControl(personEdit.dateOfBirth ?? null),
        placeOfBirth: new FormControl(personEdit.placeOfBirth ?? null),
        dateOfDeath: new FormControl(personEdit.dateOfDeath ?? null),
        placeOfDeath: new FormControl(personEdit.placeOfDeath ?? null)

      });
    }
  };

  ngOnChanges() {
    if (this.personSelected == null) {
      //en teoria ya esta creada el form persona, tal vez lo uso si pongo crear despues de haber seleccionado a alguien
      this.people = this.CreateForm(null);
    } else {
      this.people = this.CreateForm(this.personSelected);// this.personSelected;
    }
  }


  SavePerson(people: FormGroup) {
    this.person = people.value as Person;

    if (this.person.dateOfBirth != undefined) {
      this.person.dateOfBirth = this.dataSer.CalibrateDate(this.person.dateOfBirth);
    }
    if (this.person.dateOfDeath != undefined) {
      this.person.dateOfDeath = this.dataSer.CalibrateDate(this.person.dateOfDeath);
    }
    if (this.personSelected != undefined) {
      this.person.father = this.personSelectedData2.personSelected;
      this.person.mother = this.personSelectedData3.personSelected;
      this.person.eventId = this.personSelected.eventId;
    }
    else {
      if (this.personSelectedData2 != undefined) {
        this.person.father = this.personSelectedData2.personSelected;
      }
      if (this.personSelectedData3 != undefined) {
        this.person.mother = this.personSelectedData3.personSelected;
      }
    }

    if (this.buttonAction == "Update") {
      this.service.UpdatePerson(this.personSelected.id, this.person)
        .pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMPersonFinished();
          },
          error => console.log('Error Getting Position: ', error)
        );
    }
    else {

      this.service.AddPerson(this.person).pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMPersonFinished();

          },
          error => console.log('Error Getting Position: ', error)
        );
    }

  }

  ABMPersonFinished() {
    this.service.sendUpdateObject(true);
  }

  ResetForm() {
    this.people = this.CreateForm(null);
  }

  Cancel() {
    this.service.sendUpdateObject(true);
  }



}



