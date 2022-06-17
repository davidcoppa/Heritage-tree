import { AfterViewInit, Component, OnInit } from '@angular/core';
import { first } from 'rxjs';
import { EventType } from 'src/app/model/eventType.model';
import { AppService } from 'src/app/server/app.service';

@Component({
  selector: 'app-eventtypelist',
  templateUrl: './eventtypelist.component.html',
  styleUrls: ['./eventtypelist.component.css']
})
export class EventtypelistComponent implements OnInit,AfterViewInit {
  abmEventType:boolean=false;
  eventData:EventType[]=[];
  constructor(private appService: AppService,) { }

  ngOnInit(): void {
  }
  ngAfterViewInit() {
    this.eventData = this.appService.GetEventType().pipe(first())
    .subscribe(
      data => {
        //   console.log(data);
        //  var pp = new Document{ data };
        // this.dataSource = new MatTableDataSource<Clients>();

        // this.GetAll();

      //  this.service.sendUpdate();


        return;
      },
      (err) => {

        console.log(err);

      });

  }


  editEvent(contact: EventType) {
    this.abmEventType=true;   

  }
}
