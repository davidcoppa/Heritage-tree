import { Component, Inject, Input, OnChanges, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { first } from 'rxjs';
import { Person } from 'src/app/model/person.model';
import { AppService } from 'src/app/server/app.service';
import { Gender } from 'src/app/helpers/enums/gender.enum';

@Component({
  selector: 'app-people-abm',
  templateUrl: './people-abm.component.html',
  styleUrls: ['./people-abm.component.css'],
 
})
export class PeopleABMComponent implements OnInit, OnChanges {
  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;
  @Input() personSelected: Person;

  people: FormGroup;
  fb: FormBuilder;
  genderList = Gender;
  person: Person;

  constructor(fb: FormBuilder, private appService: AppService) {
    this.fb = fb;
  }

  ngOnInit(): void {
    this.people = this.CreateForm(null);
  }

  CreateForm(personEdit:Person|null): FormGroup {
    if(personEdit==null)
    {
      return this.fb.group({
        firstName: [null, [Validators.required]],
        secondName: [null],
        firstSurname: [null],
        secondSurname: [null],
        sex: [null],
        order: [null],
        dateBirth: [null],
        placeOfBirth: [null],
        dateDeath: [null],
        placeOfDeath: [null]
  
      });
    }
    else{
      return this.fb.group({
        firstName: new FormControl(personEdit.firstName ?? null),
        secondName: new FormControl(personEdit.secondName ?? null),
        firstSurname:new FormControl(personEdit.firstSurname ?? null),
        secondSurname: new FormControl(personEdit.secondSurname ?? null),
        sex: new FormControl(personEdit.sex ?? null),
        order: new FormControl(personEdit.order ?? null),
        dateBirth:new FormControl(personEdit.dateBirth ?? null),
        placeOfBirth:new FormControl(personEdit.placeOfBirth ?? null),
        dateDeath: new FormControl(personEdit.dateDeath ?? null),
        placeOfDeath: new FormControl(personEdit.placeOfDeath ?? null)
  
      });
    }
    
    
  };

  ngOnChanges() {


    if (this.personSelected == null) {

      //en teoria ya esta creada el form persona, tal vez lo uso si pongo crear despues de haber seleccionado a alguien
      this.people = this.CreateForm(null);

    } else {
      this.person = this.personSelected;

    }
  }


  SavePerson(people: FormGroup) {
    this.person = people.value as Person;

    this.appService.AddPerson(this.person).pipe(first())
      .subscribe(
        {
          next(peson) {
            console.log('Current Position: ', peson);
          },
          error(msg) {
            console.log('Error Getting Location: ', msg);
          }
        }
      );
  }



}
