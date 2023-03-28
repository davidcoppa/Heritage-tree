import { Component, Input, OnInit } from "@angular/core";
import { UntypedFormControl } from "@angular/forms";
import { map, startWith, Observable, first } from "rxjs";
import { displayPeople } from "../../../model/displayPeople.model";
import { EventType } from "../../../model/eventType.model";
import { AppService } from "../../../server/app.service";

@Component({
  selector: 'app-filter-EventType',
  templateUrl: './filterEventType.component.html'
})
export class FilterEventTypeComponent implements OnInit {

  @Input() dataEventType: EventType;

  selectedEventType: EventType;

  optionsEventType: EventType[];
  eventTypesControl = new UntypedFormControl('');
  EventTypesOptions: Observable<EventType[]>;

  personSelectedData: displayPeople;


  constructor(private service: AppService) {
    this.personSelectedData = new displayPeople();
  }

  ngOnInit(): void {
    this.GetAllEventTypes()
  }

  GetAllEventTypes() {
    console.log('event type list');

    this.service.GetEventType('Title', 'desc', 0, 10000, '')
      .pipe(first())
      .subscribe(
        data => {
          console.log('Current data filter event type: ', data);

          this.optionsEventType = data;

          this.EventTypesOptions = this.eventTypesControl.valueChanges.pipe(
            startWith(''),
            map(value => {
              const name = typeof value === 'string' ? value : value?.name;
              return name ? this._filterEventTypes(name as string) : this.optionsEventType.slice();
            }),
          );

        },
        error => console.log('Error Getting Position: ', error)
      );
  }

  displayEventType = (user: EventType): string => {

    if (this.dataEventType != undefined) {
      if (user.id != undefined) {
        this.dataEventType = user;
        this.service.sendUpdatePeople(this.dataEventType);
      } else {
        this.eventTypesControl.setValue(this.dataEventType);
      }
    }

    this.service.sendUpdateEventType(user);

    return user && user.name ? user.name : '';
  }

  private _filterEventTypes(name: string): EventType[] {
    const filterValue = name.toLowerCase();

    return this.optionsEventType.filter(option => option.name.toLowerCase().includes(filterValue));
  }




}
