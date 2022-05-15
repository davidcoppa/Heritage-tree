import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { SortDirection } from "@angular/material/sort";
import { PersonApi } from "../model/person.api.model";
 
@Injectable()
export class AppService {
    constructor(private httpClient: HttpClient) {
    }
 
    // getSampleData(sort: string, order: SortDirection, page: number, q: string): Observable<GithubApi> {
    //     return this.httpClient.get<GithubApi>(`https://api.github.com/search/issues?q=${q}&sort=${sort}&order=${order}&page=${page + 1}`);
    // }

    getPeople(sort: string, order: SortDirection, page: number, search: string): Observable<PersonApi> {
        let queryParams = new HttpParams();
        queryParams = queryParams.append("sort",sort);
        queryParams = queryParams.append("order",order);
        queryParams = queryParams.append("page",page??0);
        queryParams = queryParams.append("search",search);

//?search=${search}&sort=${sort}&order=${order}&page=${page + 1}
            return this.httpClient.get<PersonApi>('api/People/GetFilter',{ params:queryParams});
    }
}