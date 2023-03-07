import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { first, Subscription } from 'rxjs';
import { Events } from 'src/app/model/event.model';
import { AppService } from 'src/app/server/app.service';
import { CustomDateAdapterService } from '../../../helpers/dates/CustomDateAdapterService';
import { PersonEnum } from '../../../helpers/enums/person.enum';
import { displayPeople } from '../../../model/displayPeople.model';
import { EventType } from '../../../model/eventType.model';
import { ListObject } from '../../../model/listObject.model';

@Component({
  selector: 'app-event-abm',
  templateUrl: './event-abm.component.html',
  styleUrls: ['./event-abm.component.css']
})
export class EventAbmComponent implements OnInit, OnDestroy {
  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;
  @Input() eventSelected: Events;
  @Input() abmEvent: boolean;

  event: UntypedFormGroup;
  fb: UntypedFormBuilder;
  evt: Events;
  buttonAction: string = "Add";

  //listModel: ListObject;

  selectedEventType: EventType;

  personSelectedData1: displayPeople;
  personSelectedData2: displayPeople;
  personSelectedData3: displayPeople;

  private subscriptionPeopleFilter: Subscription;
  private subscriptionEventFilter: Subscription;

  constructor(fb: UntypedFormBuilder,
    private service: AppService,
    private dateAdapter: DateAdapter<Date>,
    private dataSer: CustomDateAdapterService) {

    this.dateAdapter.setLocale('en-GB'); //dd/MM/yyyy

    this.fb = fb;

    this.subscriptionPeopleFilter = this.service.getUpdatePeople().subscribe
      (data => { //message contains the data sent from service
        console.log("sendUpdatePeople: " + data.data);
        switch (data.data.personEnum) {
          case PersonEnum.son: {

            this.personSelectedData1 = data.data;
            break;
          }
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

    this.subscriptionEventFilter = this.service.getUpdateEventType().subscribe
      (data => { //message contains the data sent from service
        console.log("sendUpdateEvent: " + data.data);
        this.selectedEventType = data.data;
      });
  }

  ngOnDestroy() {
    this.subscriptionPeopleFilter.unsubscribe();
    this.subscriptionEventFilter.unsubscribe();
  }

  ngOnInit(): void {

    if (this.eventSelected != undefined) {
      this.event = this.CreateForm(this.eventSelected);
    } else {
      this.event = this.CreateForm(null);
    }

  }

  CreateForm(eventEdit: Events | null): UntypedFormGroup {
   

    this.personSelectedData1 = new displayPeople();
    this.personSelectedData2 = new displayPeople();
    this.personSelectedData3 = new displayPeople();

    if (eventEdit == null) {

      this.personSelectedData1.personEnum = PersonEnum.son;
      this.personSelectedData2.personEnum = PersonEnum.father;
      this.personSelectedData3.personEnum = PersonEnum.mother;


      return this.fb.group({
        title: [null, [Validators.required]],
        //description: [null],
        eventDate: [null],
        eventType: [null],
        person1: [null],
        person2: [null],
        person3: [null],
        location: [null],
        media: [null]
      });
    } else {
      this.evt = this.eventSelected
      this.buttonAction = "Update";

      this.selectedEventType = eventEdit.eventType;

      this.personSelectedData1.personEnum = PersonEnum.son;
      this.personSelectedData1.personSelected = eventEdit.person1;
      this.personSelectedData2.personEnum = PersonEnum.father;
      this.personSelectedData2.personSelected = eventEdit.person2;
      this.personSelectedData3.personEnum = PersonEnum.mother;
      this.personSelectedData3.personSelected = eventEdit.person3;


      return this.fb.group({
        title: new UntypedFormControl(eventEdit.title ?? null),
        //description: new UntypedFormControl(eventEdit.description ?? null),
        eventDate: new UntypedFormControl(eventEdit.eventDate ?? null),
        eventType: new UntypedFormControl(eventEdit.eventType ?? null),
        person1: new UntypedFormControl(eventEdit.person1 ?? null),
        person2: new UntypedFormControl(eventEdit.person2 ?? null),
        person3: new UntypedFormControl(eventEdit.person3 ?? null),
        location: new UntypedFormControl(eventEdit.location ?? null),
        media: new UntypedFormControl(eventEdit.media ?? null)

      });
    }

  }

  ngOnChanges() {
    if (this.eventSelected == null) {
      //en teoria ya esta creada el form evento, tal vez lo uso si pongo crear despues de haber seleccionado a alguien
      this.event = this.CreateForm(null);
    } else {
      this.evt = this.eventSelected;
   //   this.event = this.CreateForm(this.evt);

    }
  }

  SaveEvent(EventABM: UntypedFormGroup) {
    this.evt = EventABM.value as Events;
    

    this.evt.eventType = this.selectedEventType;

  

    console.log('Current evt: ', this.evt);



    if (this.evt.eventDate != undefined) {
      this.evt.eventDate = this.dataSer.CalibrateDate(this.evt.eventDate);
    }


    if (this.buttonAction == "Update") {
      this.evt.person1 = this.personSelectedData1.personSelected;
      this.evt.person2 = this.personSelectedData2.personSelected;
      this.evt.person3 = this.personSelectedData3.personSelected;
      this.evt.id = this.eventSelected.id;

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
      this.evt.person1 = this.personSelectedData1.personSelected;
      this.evt.person2 = this.personSelectedData2.personSelected;
      this.evt.person3 = this.personSelectedData3.personSelected;


      if (!this.evt.title.includes(this.evt.person1.firstName)) {
        this.evt.title += " "+this.evt.person1.firstName;
      }


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

}


