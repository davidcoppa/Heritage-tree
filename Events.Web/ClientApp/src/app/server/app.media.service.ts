import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SortDirection } from "@angular/material/sort";
import { Observable, Subject } from "rxjs";
import { Media } from "../model/media.model";






@Injectable()
export class AppMediaService {

  constructor(private httpClient: HttpClient) {
  }

  subjectMedia = new Subject<any>();

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


  //media visualization

  allImages = [];  
  getImages() {
    return this.allImages = Imagesdelatils.slice(0);
  }

  getImage(id: number) {
    return Imagesdelatils.slice(0).find(Images => Images.id == id)
  }
}
const Imagesdelatils = [
  { "id": 1, "brand": "Apple", "url": "assets/Images/Macbook1.jpg" },
  { "id": 2, "brand": "Apple", "url": "assets/Images/MacBook.jpg" },
  { "id": 3, "brand": "Apple", "url": "assets/Images/laptop3.jpg" },
  { "id": 4, "brand": "Apple", "url": "assets/Images/laptop4.jpg" },
  { "id": 5, "brand": "hp", "url": "assets/Images/hp1.jpg" },
  { "id": 6, "brand": "hp", "url": "assets/Images/hp2.jpg" },
  { "id": 7, "brand": "hp", "url": "assets/Images/hp3.jpg" },
  { "id": 8, "brand": "hp", "url": "assets/Images/hp4.jpg" },
  { "id": 9, "brand": "Lenovo", "url": "assets/Images/laptop5.jpg" },
  { "id": 10, "brand": "Lenovo", "url": "assets/Images/laptop7.jpg" },
  { "id": 11, "brand": "Lenovo", "url": "assets/Images/laptop8.jpg" },
  { "id": 12, "brand": "Lenovo", "url": "assets/Images/laptop9.jpg" },
  { "id": 13, "brand": "Lenovo", "url": "assets/Images/laptop11.jpg" },
  { "id": 14, "brand": "asus", "url": "assets/Images/laptop13.jpg" },
  { "id": 15, "brand": "asus", "url": "assets/Images/laptop14.jpg" },
  { "id": 16, "brand": "asus", "url": "assets/Images/laptop15.jpg" },
  { "id": 17, "brand": "asus", "url": "assets/Images/laptop16.jpg" },
  { "id": 18, "brand": "asus", "url": "assets/Images/laptop17.jpg" },
  { "id": 19, "brand": "asus", "url": "assets/Images/laptop18.jpg" },
  { "id": 20, "brand": "asus", "url": "assets/Images/laptop20.jpg" },

]    
