import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs';
import { EventType } from '../../../model/eventType.model';
import { ListObject } from '../../../model/listObject.model';
import { AppService } from '../../../server/app.service';

@Component({
  selector: 'app-eventtype-abm',
  templateUrl: './eventtype-abm.component.html',
  styleUrls: ['./eventtype-abm.component.css']
})
export class EventtypeAbmComponent implements OnInit {

  @Input() eventTypeSelected: EventType;
  @Input() abmEventType: boolean;

  eventTypesGroup: UntypedFormGroup;
  fb: UntypedFormBuilder;
  eventTypes: EventType;

  buttonAction: string = "Add";
  listModel: ListObject;


  constructor(fb: UntypedFormBuilder, private service: AppService) {
    this.fb = fb;

  }

  ngOnInit(): void {
    if (this.eventTypeSelected != undefined) {
      this.eventTypesGroup = this.CreateForm(this.eventTypeSelected);

    } else {
      this.eventTypesGroup = this.CreateForm(null);

    }
  }

  CreateForm(eventTypeEdit: EventType | null): UntypedFormGroup {
    if (eventTypeEdit == null) {

      return this.fb.group({
        name: [null, [Validators.required]],
        description: [null]

      });
    }
    else {
      this.eventTypes = this.eventTypeSelected;
      this.buttonAction = "Update";

      return this.fb.group({
        id: new UntypedFormControl(this.eventTypeSelected.id),
        name: new UntypedFormControl(eventTypeEdit.name ?? null),
        description: new UntypedFormControl(eventTypeEdit.description ?? null),

      });
    }
  };
  ngOnChanges() {
    if (this.eventTypeSelected == null) {
      //en teoria ya esta creada el form persona, tal vez lo uso si pongo crear despues de haber seleccionado a alguien
      this.eventTypesGroup = this.CreateForm(null);
    } else {
      this.eventTypesGroup = this.CreateForm(this.eventTypeSelected);// this.personSelected;
    }
  }


  SaveEventType(evt: UntypedFormGroup) {
    this.eventTypes = evt.value as EventType;


    if (this.buttonAction == "Update") {
      this.service.UpdateEventType(this.eventTypeSelected.id, this.eventTypes)
        .pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMPEventTypeFinished();
          },
          error => console.log('Error Getting Position: ', error)
        );
    }
    else {

      this.service.AddEventType(this.eventTypes).pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMPEventTypeFinished();

          },
          error => console.log('Error Getting Position: ', error)
        );
    }

  }





  ABMPEventTypeFinished() {
    this.service.sendUpdateObject(true);
  }

  ResetForm() {
    this.eventTypesGroup = this.CreateForm(null);
  }

  Cancel() {
    this.service.sendUpdateObject(true);
  }


}
