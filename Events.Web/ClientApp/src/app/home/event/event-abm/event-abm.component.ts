import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { first } from 'rxjs';
import { Events } from 'src/app/model/event.model';
import { AppService } from 'src/app/server/app.service';

@Component({
  selector: 'app-event-abm',
  templateUrl: './event-abm.component.html',
  styleUrls: ['./event-abm.component.css']
})
export class EventAbmComponent implements OnInit {
  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;
  @Input() eventSelected: Events;

  event: FormGroup;
  fb: FormBuilder;
  evt:Events;

  constructor(fb: FormBuilder, private appService: AppService) {
    this.fb = fb;
  }

  ngOnInit(): void {
    this.event=this.CreateForm(null);
  }

  CreateForm(eventEdit:Events| null): FormGroup {
    if(eventEdit==null)
    {
      return this.fb.group({
        Title: [null, [Validators.required]],
        Description: [null],
        EventDate: [null],
        EventType: [null],
        Person1: [null],
        Person2: [null],
        Person3: [null],
        Location: [null],
        Media: [null]
      });
    }else{
      return this.fb.group({
        Title: new FormControl(eventEdit.Title ?? null),
        Description: new FormControl(eventEdit.Description ?? null),
        EventDate:new FormControl(eventEdit.EventDate ?? null),
        EventType: new FormControl(eventEdit.EventType ?? null),
        Person1: new FormControl(eventEdit.Person1 ?? null),
        Person2: new FormControl(eventEdit.Person2 ?? null),
        Person3:new FormControl(eventEdit.Person3 ?? null),
        Location:new FormControl(eventEdit.Location ?? null),
        Media: new FormControl(eventEdit.Media ?? null)
  
      });    }
  
  }

  ngOnChanges() {


    if (this.eventSelected == null) {

      //en teoria ya esta creada el form evento, tal vez lo uso si pongo crear despues de haber seleccionado a alguien
      this.event = this.CreateForm(null);

    } else {
      this.evt = this.eventSelected;

    }
  }

  SaveEvent(EventABM: FormGroup) {
    this.evt = EventABM.value as Events;

    this.appService.AddEvent(this.evt).pipe(first())
      .subscribe(
        {
          next(data) {
            console.log('Current Position: ', data);
          },
          error(msg) {
            console.log('Error Getting Location: ', msg);
          }
        }
      );
  }

}
