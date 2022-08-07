import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { first, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import { Events } from 'src/app/model/event.model';
import { AppService } from 'src/app/server/app.service';
import { CustomDateAdapterService } from '../../../helpers/dates/CustomDateAdapterService';
import { EventType } from '../../../model/eventType.model';
import { ListObject } from '../../../model/listObject.model';

@Component({
  selector: 'app-event-abm',
  templateUrl: './event-abm.component.html',
  styleUrls: ['./event-abm.component.css']
})
export class EventAbmComponent implements OnInit {
  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;
  @Input() eventSelected: Events;
  @Input() abmEvent: boolean;

  event: FormGroup;
  fb: FormBuilder;
  evt: Events;
  buttonAction: string = "Add";
  listModel: ListObject;

  selectedEventType: string[];
  options: EventType[];

  //: string;

  constructor(fb: FormBuilder, private service: AppService, private dateAdapter: DateAdapter<Date>, private dataSer: CustomDateAdapterService) {
    this.dateAdapter.setLocale('en-GB'); //dd/MM/yyyy

    this.fb = fb;
  }

  ngOnInit(): void {
    this.GetAllEventTypes();

    if (this.eventSelected != undefined) {
      this.event = this.CreateForm(this.eventSelected);
    } else {
      this.event = this.CreateForm(null);
    }

  }

  CreateForm(eventEdit: Events | null): FormGroup {
    if (eventEdit == null) {
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
    } else {
      this.evt = this.eventSelected
      this.buttonAction = "Update";

      return this.fb.group({
        Title: new FormControl(eventEdit.title ?? null),
        Description: new FormControl(eventEdit.description ?? null),
        EventDate: new FormControl(eventEdit.eventDate ?? null),
        EventType: new FormControl(eventEdit.eventType ?? null),
        Person1: new FormControl(eventEdit.person1 ?? null),
        Person2: new FormControl(eventEdit.person2 ?? null),
        Person3: new FormControl(eventEdit.person3 ?? null),
        Location: new FormControl(eventEdit.location ?? null),
        Media: new FormControl(eventEdit.media ?? null)

      });
    }

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

    if (this.evt.eventDate != undefined) {
      this.evt.eventDate = this.dataSer.CalibrateDate(this.evt.eventDate);
    }


    if (this.buttonAction == "Update") {
      this.service.UpdateEvent(this.eventSelected.id, this.evt)
        .pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMEventFinished();
          },
          error => console.log('Error Getting Position: ', error)
        );
    }
    else {

      this.service.AddEvent(this.evt).pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMEventFinished();

          },
          error => console.log('Error Getting Position: ', error)
        );
    }
  }

  ABMEventFinished() {
    this.service.sendUpdateObject(true);
  }

  ResetForm() {
    this.event = this.CreateForm(null);
  }

  Cancel() {
    this.service.sendUpdateObject(true);
  }

  myControl = new FormControl('');
 // options: EventType[] = [{ name: 'Mary' }, { name: 'Shelley' }, { name: 'Igor' }];

  EventTypesOptions: Observable<EventType[]>;

  //EventTypesOptions
  GetAllEventTypes() {
    console.log('event type list');

    this.service.GetEventType('Title', 'desc', 0, 10000, '')
      .pipe(first())
      .subscribe(
        data => {
          console.log('Current data: ', data);

          this.options = data;

          this.EventTypesOptions = this.myControl.valueChanges.pipe(
            startWith(''),
            map(value => {
              const name = typeof value === 'string' ? value : value?.name;
              return name ? this._filter(name as string) : this.options.slice();
            }),
          );

        },
        error => console.log('Error Getting Position: ', error)
    );


    
  }

  displayFn(user: EventType): string {
    return user && user.name ? user.name : '';
  }

  private _filter(name: string): EventType[] {
    const filterValue = name.toLowerCase();

    return this.options.filter(option => option.name.toLowerCase().includes(filterValue));
  }
}


