import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SortDirection } from "@angular/material/sort";
import { Observable, Subject } from "rxjs";
import { Media } from "../model/media.model";






@Injectable()
export class AppMediaService {

  constructor(private httpClient: HttpClient) {
  }


  subjectMedia = new Subject<any>(); //need to create a subject


  getUpdateMedia(): Observable<any> {
    return this.subjectMedia.asObservable();
  }
  sendUpdateMedia(file: any) {
    this.subjectMedia.next({ data: file });
  }

  private GetParams(sort: string, order: string, page: number, itemsPage: number, search: string) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("sort", sort); //column
    queryParams = queryParams.append("order", order);
    queryParams = queryParams.append("page", page ?? 0);
    queryParams = queryParams.append("itemsPage", itemsPage ?? 10);
    queryParams = queryParams.append("search", search);
    return queryParams;
  }

  //TODO: add media type
  getMedia(sort: string, order: SortDirection, page: number, itemsPage: number, search: string): Observable<Media[]> {
    let queryParams = this.GetParams(sort, order, page, itemsPage, search);

    return this.httpClient.get<Media[]>('api/Media/GetFilter', { params: queryParams });
  }

  AddMedia(media: Media): Observable<Media> {
    return this.httpClient.post<Media>('api/Media/Create', media);
  }

  UpdateMedia(id: number, newMedia: Media): Observable<Media> {

    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", id);

    return this.httpClient.post<Media>('api/Media/EditMedia', newMedia, { params: queryParams });

  }

}
