import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { SortDirection } from "@angular/material/sort";
import { PersonApi } from "../model/person.api.model";
import { Person } from "../model/person.model";
import { EventApi } from "../model/event.api";

@Injectable()
export class AppService {

    constructor(private httpClient: HttpClient) {
    }

    getPeople(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<PersonApi> {
        let queryParams = new HttpParams();
        queryParams = queryParams.append("sort", sort);//column
        queryParams = queryParams.append("order", order);
        queryParams = queryParams.append("page", page ?? 0);
        queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
        queryParams = queryParams.append("search", search);

        //?search=${search}&sort=${sort}&order=${order}&page=${page + 1}
        return this.httpClient.get<PersonApi>('api/People/GetFilter', { params: queryParams });
    }


    AddPerson(newPerson: Person): Observable<Person> {

        return this.httpClient.post<Person>('api/People/Create', newPerson);

    }
    getEvents(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<EventApi> 
        {
            let queryParams = new HttpParams();
            queryParams = queryParams.append("sort", sort);//column
            queryParams = queryParams.append("order", order);
            queryParams = queryParams.append("page", page ?? 0);
            queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
            queryParams = queryParams.append("search", search);
    
            //?search=${search}&sort=${sort}&order=${order}&page=${page + 1}
            return this.httpClient.get<EventApi>('api/Event/GetFilter', { params: queryParams });
        }

    }