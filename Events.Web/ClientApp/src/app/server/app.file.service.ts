import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Media } from '../model/media.model';

@Injectable()
export class AppFileService {
  constructor(private httpClient: HttpClient,
  ) { }


  subjectName = new Subject<any>(); 

  sendUpdateObject(file: any) { 
    this.subjectName.next({ data: file }); 
  }

  //sendUpdateFile() {
  //  this.subjectName.next({});
  //}
  getUpdateFile(): Observable<any> { 
    return this.subjectName.asObservable(); 
  }

  sendUrlDataFile(url: any) {
    this.subjectName.next({ data: url });
  }

 
  //GetMediaItems(getAll: boolean): Observable<any> {

  //  return this.httpClient.get<Document[]>("api/Files/GetBlogItems",
  //    {
  //      params: {
  //        getAll: getAll.toString()
  //      }
  //    });
  //}




  SendFile(document: Media): Observable<any> {
    return this.httpClient.post("api/Files/UploadDataFile", document);

  }

  UploadFile(formData: any): Observable<any> {
    return this.httpClient.post("api/Files/UploadFile", formData, {
      reportProgress: true,
      observe: 'events'
    })

  }

  GetDocumentsList(): Observable<any> {
    return this.httpClient.get<Media[]>("api/Files/GetDocumentsList");
  }


}
