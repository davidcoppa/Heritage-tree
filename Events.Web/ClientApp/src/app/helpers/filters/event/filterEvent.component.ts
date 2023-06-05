import { Component, Input, OnInit } from "@angular/core";
import { UntypedFormControl } from "@angular/forms";
import { map, startWith, Observable, first } from "rxjs";
import { Events } from "../../../model/event.model";
import { AppService } from "../../../server/app.service";

@Component({
  selector: 'app-filter-Event',
  templateUrl: './filterEvent.component.html'
})
export class FilterEventComponent implements OnInit {

  @Input() dataEvent: Events;

  //selectedEvent: Events;

  optionsEvent: Events[];
  eventControl = new UntypedFormControl('');
  EventOptions: Observable<Events[]>;

 


  constructor(private service: AppService) {
   
  }

  ngOnInit(): void {
    this.GetAllEvents()
  }

  GetAllEvents() {
    console.log('event list');

    this.service.getEvents('Title', 'desc', 0, 10000, '')
      .pipe(first())
      .subscribe(
        data => {
       //   console.log('Current data filter event type: ', data);

          this.optionsEvent = data;

          this.EventOptions = this.eventControl.valueChanges.pipe(
            startWith(''),
            map(value => {
              const name = typeof value === 'string' ? value : value?.name;
              return name ? this._filterEvents(name as string) : this.optionsEvent.slice();
            }),
          );

        },
        error => console.log('Error Getting Position: ', error)
      );
  }

  displayEvent = (user: Events): string => {

    if (this.dataEvent != undefined) {
      if (user.id != undefined) {
        this.dataEvent = user;
        this.service.sendUpdateEvent(this.dataEvent);
      } else {
        this.eventControl.setValue(this.dataEvent);
      }
    }

    //this.service.sendUpdateEvent(user);

    return user && user.description ? user.description : '';
  }

  private _filterEvents(name: string): Events[] {
    const filterValue = name.toLowerCase();

    return this.optionsEvent.filter(option => option.description.toLowerCase().includes(filterValue));
  }




}
