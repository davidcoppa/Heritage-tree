import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { SortDirection } from "@angular/material/sort";
import { Person } from "../model/person.model";
import { Events } from "../model/event.model";
import { EventType } from "../model/eventType.model";

@Injectable()
export class AppService {



    constructor(private httpClient: HttpClient) {
    }

    getPeople(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<Person[]> {
        let queryParams = new HttpParams();
        queryParams = queryParams.append("sort", sort);//column
        queryParams = queryParams.append("order", order);
        queryParams = queryParams.append("page", page ?? 0);
        queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
        queryParams = queryParams.append("search", search);

        //?search=${search}&sort=${sort}&order=${order}&page=${page + 1}
        return this.httpClient.get<Person[]>('api/People/GetFilter', { params: queryParams });
    }


    AddPerson(newPerson: Person): Observable<Person> {

        return this.httpClient.post<Person>('api/People/Create', newPerson);

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

    AddPEvent(evt: Events) : Observable<Events> {
        return this.httpClient.post<Events>('api/Event/Create', evt);
    }

    GetEventType() : Observable<EventType[]> {
        return this.httpClient.get<EventType[]>('api/EventTypes/Get');
      }

}