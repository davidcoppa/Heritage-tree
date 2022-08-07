import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, Subject } from "rxjs";
import { SortDirection } from "@angular/material/sort";
import { Person } from "../model/person.model";
import { Events } from "../model/event.model";
import { EventType } from "../model/eventType.model";
import { Media } from "../model/media.model";

@Injectable()
export class AppService {



  constructor(private httpClient: HttpClient) {
  }

  subjectName = new Subject<any>(); //need to create a subject

  sendUpdateObject(file: any) { //the component that wants to update something, calls this fn
    this.subjectName.next({ data: file }); //next() will feed the value in Subject
  }
  sendUpdate() {
    this.subjectName.next({});
  }
  getUpdate(): Observable<any> { //the receiver component calls this function 
    return this.subjectName.asObservable(); //it returns as an observable to which the receiver funtion will subscribe
  }




  getPeople(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<Person[]> {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("sort", sort);//column
    queryParams = queryParams.append("order", order);
    queryParams = queryParams.append("page", page ?? 0);
    queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
    queryParams = queryParams.append("search", search);

    return this.httpClient.get<Person[]>('api/People/GetFilter', { params: queryParams });
  }


  AddPerson(newPerson: Person): Observable<Person> {

    return this.httpClient.post<Person>('api/People/Create', newPerson);

  }

  UpdatePerson(id: number, newPerson: Person): Observable<Person> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<Person>('api/People/Edit'
                                                       , newPerson
                                                        ,{ params: queryParams });

  }

  getEvents(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<Events[]> {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("sort", sort);//column
    queryParams = queryParams.append("order", order);
    queryParams = queryParams.append("page", page ?? 0);
    queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
    queryParams = queryParams.append("search", search);

    //?search=${search}&sort=${sort}&order=${order}&page=${page + 1}
    return this.httpClient.get<Events[]>('api/Event/GetFilter', { params: queryParams });
  }

  AddEvent(evt: Events): Observable<Events> {
    return this.httpClient.post<Events>('api/Event/Create', evt);
  }

  UpdateEvent(id: number, newEvent: Events): Observable<Events> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<Events>('api/Event/Edit'
      , newEvent
      , { params: queryParams });

  }

  GetEventType(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<EventType[]> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("sort", sort);//column
    queryParams = queryParams.append("order", order);
    queryParams = queryParams.append("page", page ?? 0);
    queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
    queryParams = queryParams.append("search", search);


    return this.httpClient.get<EventType[]>('api/EventTypes/Get', { params: queryParams });
  }
  UpdateEventType(id: number, newEventType: EventType): Observable<EventType> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<EventType>('api/EventTypes/Edit'
      , newEventType
      , { params: queryParams });

  }
  AddEventType(newEventType: EventType): Observable<EventType> {

    return this.httpClient.post<EventType>('api/EventTypes/Create', newEventType);

  }





  //TODO: add media type
  getMedia(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<Media[]> {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("sort", sort);//column
    queryParams = queryParams.append("order", order);
    queryParams = queryParams.append("page", page ?? 0);
    queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
    queryParams = queryParams.append("search", search);

    //?search=${search}&sort=${sort}&order=${order}&page=${page + 1}
    return this.httpClient.get<Media[]>('api/Media/GetFilter', { params: queryParams });
  }

  AddMedia(media: Media): Observable<Media> {
    return this.httpClient.post<Media>('api/Media/Create', media);
  }

}
