import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, Subject } from "rxjs";
import { SortDirection } from "@angular/material/sort";
import { Person } from "../model/person.model";
import { Events } from "../model/event.model";
import { EventType } from "../model/eventType.model";
import { Media } from "../model/media.model";
import { Country } from "../model/country.model";
import { State } from "../model/state.model";
import { City } from "../model/city.model";

@Injectable()
export class AppService {



  constructor(private httpClient: HttpClient) {
  }

  subjectName = new Subject<any>(); //need to create a subject
  subjectPeople = new Subject<any>(); 
  subjectEventType = new Subject<any>(); 
  subjectCountry = new Subject<any>(); 
  subjectInnerState = new Subject<any>(); 
  subjectInnerCity = new Subject<any>(); 

  sendUpdateObject(file: any) { //the component that wants to update something, calls this fn
    this.subjectName.next({ data: file }); //next() will feed the value in Subject
  }
  sendUpdate() {
    this.subjectName.next({});
  }
  getUpdate(): Observable<any> { //the receiver component calls this function 
    return this.subjectName.asObservable(); //it returns as an observable to which the receiver funtion will subscribe
  }

  //People filter list
  getUpdatePeople(): Observable<any> { 
    return this.subjectPeople.asObservable();
  }
  sendUpdatePeople(file: any) { 
    this.subjectPeople.next({ data: file }); 
  }

  //EventType filter list
  getUpdateEventType(): Observable<any> { 
    return this.subjectEventType.asObservable(); 
  }
  sendUpdateEventType(file: any) { 
    this.subjectEventType.next({ data: file }); 
  }

  //Country filter lisr
  getUpdateCountry(): Observable<any> {
    return this.subjectCountry.asObservable();
  }
  sendUpdateCountry(file: any) {
    this.subjectCountry.next({ data: file });
  }

  //state filter
  getUpdateInnerTableState(): Observable<any> {
    return this.subjectInnerState.asObservable();
  }
  sendUpdateInnerTableState(file: any) {
    this.subjectInnerState.next({ data: file });
  }

  //sendUpdateInnerTableCity

  getsendUpdateInnerTableCity(): Observable<any> {
    return this.subjectInnerCity.asObservable();
  }
  sendUpdateInnerTableCity(file: any) {
    this.subjectInnerCity.next({ data: file });
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
    console.log('event list');
    let queryParams = new HttpParams();
    queryParams = queryParams.append("sort", sort);//column
    queryParams = queryParams.append("order", order);
    queryParams = queryParams.append("page", page ?? 0);
    queryParams = queryParams.append("search", search);

    queryParams = queryParams.append("itemsPage", itemsPage ?? 10);

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


  //GetCountries

  GetCountries(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<Country[]> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("sort", sort);//column
    queryParams = queryParams.append("order", order);
    queryParams = queryParams.append("page", page ?? 0);
    queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
    queryParams = queryParams.append("search", search);


    return this.httpClient.get<Country[]>('api/Location/GetFilter', { params: queryParams });
  }


  UpdateCountries(id: number, newLocation: Country): Observable<Country> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<Country>('api/Location/Edit'
      , newLocation
      , { params: queryParams });

  }
  AddCountries(newLocation: Country): Observable<Country> {

    return this.httpClient.post<Country>('api/Location/Create', newLocation);

  }

  //GetStates

  GetStates(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<State[]> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("sort", sort);//column
    queryParams = queryParams.append("order", order);
    queryParams = queryParams.append("page", page ?? 0);
    queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
    queryParams = queryParams.append("search", search);


    return this.httpClient.get<State[]>('api/LocationState/GetFilterState', { params: queryParams });
  }


  UpdateStates(id: number, newLocation: State): Observable<State> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<State>('api/LocationState/EditState'
      , newLocation
      , { params: queryParams });

  }
  AddStates(newLocation: State): Observable<State> {

    return this.httpClient.post<State>('api/LocationState/CreateState', newLocation);

  }


  //GetCities

  GetCities(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<City[]> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("sort", sort);//column
    queryParams = queryParams.append("order", order);
    queryParams = queryParams.append("page", page ?? 0);
    queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
    queryParams = queryParams.append("search", search);


    return this.httpClient.get<City[]>('api/LocationCity/GetFilterState', { params: queryParams });
  }


  UpdateCity(id: number, newLocation: City): Observable<City> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<City>('api/LocationCity/EditState'
      , newLocation
      , { params: queryParams });

  }
  AddCity(newLocation: City): Observable<City> {

    return this.httpClient.post<City>('api/LocationCity/CreateState', newLocation);

  }




}
