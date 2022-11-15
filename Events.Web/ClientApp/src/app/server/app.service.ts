import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, Subject } from "rxjs";
import { SortDirection } from "@angular/material/sort";
import { Person } from "../model/person.model";
import { Events } from "../model/event.model";
import { EventType } from "../model/eventType.model";
import { Country } from "../model/country.model";
import { State } from "../model/state.model";
import { City } from "../model/city.model";
import { rootValues } from "../model/PersonWithParents.model";

@Injectable()
export class AppService {

  constructor(private httpClient: HttpClient) {
  }

  subjectName = new Subject<any>(); //need to create a subject
  subjectPeople = new Subject<any>();
  subjectEventType = new Subject<any>();
  subjectCountry = new Subject<any>();
  subjectState = new Subject<any>();
  subjectCity = new Subject<any>();
  subjectAbmLocation = new Subject<any>();

  sendUpdateObject(file: any) { //the component that wants to update something, calls this fn
    this.subjectName.next({ data: file }); //next() will feed the value in Subject
  }
  sendUpdate() {
    this.subjectName.next({});
  }
  getUpdate(): Observable<any> {
    return this.subjectName.asObservable();
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
  getUpdateABMLocation(): Observable<any> {
    return this.subjectAbmLocation.asObservable();
  }
  sendUpdateABMLocation(file: any) {
    this.subjectAbmLocation.next({ data: file });
  }

  //TODO: review
  getUpdateCountry(): Observable<any> {
    return this.subjectCountry.asObservable();
  }
  sendUpdateCountry(file: any) {
    this.subjectCountry.next({ data: file });
  }
  //state filter
  getUpdateState(): Observable<any> {
    return this.subjectState.asObservable();
  }
  sendUpdateState(file: any) {
    this.subjectState.next({ data: file });
  }

  // sendUpdateInnerTableCity

  getUpdateCity(): Observable<any> {
    return this.subjectCity.asObservable();
  }
  sendUpdateCity(file: any) {
    this.subjectCity.next({ data: file });
  }

  //get params on search
  private GetParams(sort: string, order: string, page: number, itemsPage: number, search: string) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("sort", sort); //column
    queryParams = queryParams.append("order", order);
    queryParams = queryParams.append("page", page ?? 0);
    queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
    queryParams = queryParams.append("search", search);
    return queryParams;
  }

  //Person
  getPeople(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<Person[]> {
    let queryParams = this.GetParams(sort, order, page, itemsPage, search);

    return this.httpClient.get<Person[]>('api/People/GetFilter', { params: queryParams });
  }


  AddPerson(newPerson: Person): Observable<Person> {

    return this.httpClient.post<Person>('api/People/Create', newPerson);

  }

  UpdatePerson(id: number, newPerson: Person): Observable<Person> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<Person>('api/People/Edit', newPerson, { params: queryParams });

  }

  //Events
  getEvents(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<Events[]> {
    let queryParams = this.GetParams(sort, order, page, itemsPage, search);

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

  //Event type
  GetEventType(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<EventType[]> {

    let queryParams = this.GetParams(sort, order, page, itemsPage, search);

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

  //GetCountries
  GetCountries(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<Country[]> {

    let queryParams = this.GetParams(sort, order, page, itemsPage, search);

    return this.httpClient.get<Country[]>('api/Country/GetFilterCountry', { params: queryParams });
  }


  UpdateCountries(id: number, newLocation: Country): Observable<Country> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<Country>('api/Country/EditCountry', newLocation, { params: queryParams });

  }

  AddCountries(newLocation: Country): Observable<Country> {
    return this.httpClient.post<Country>('api/Country/CreateCountry', newLocation);
  }

  //GetStates 

  GetStates(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<State[]> {
    let queryParams = this.GetParams(sort, order, page, itemsPage, search);


    return this.httpClient.get<State[]>('api/State/GetFilterState', { params: queryParams });
  }


  UpdateStates(id: number, newLocation: State): Observable<State> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<State>('api/State/EditState'
      , newLocation
      , { params: queryParams });

  }
  AddStates(newLocation: State): Observable<State> {

    return this.httpClient.post<State>('api/State/CreateState', newLocation);

  }

  //GetCities 
  GetCities(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<City[]> {

    let queryParams = this.GetParams(sort, order, page, itemsPage, search);

    return this.httpClient.get<City[]>('api/City/GetFilterCity', { params: queryParams });
  }

  UpdateCity(id: number, newLocation: City): Observable<City> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<City>('api/City/EditCity'
      , newLocation
      , { params: queryParams });

  }
  AddCity(newLocation: City): Observable<City> {

    return this.httpClient.post<City>('api/City/CreateCity', newLocation);

  }

  //GetDataToVisualize

  GetDataToVisualize(id: number): Observable<rootValues> { 
    let queryParams = new HttpParams();
    queryParams = queryParams.append("idPerson", id);

    return this.httpClient.get<rootValues>('api/ParentPersons/GetAllFilter', { params: queryParams });
  }









}
