import { HttpErrorResponse, HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, Input, EventEmitter, Output, inject } from '@angular/core';
import { Observable, Subject, Subscription } from 'rxjs';
import { catchError, finalize, map } from 'rxjs/operators';
import { OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AppFileService } from '../../../server/app.file.service';
import { Media } from '../../../model/media.model';
import { FileData } from '../../../model/fileData.model';
import { CustomDateAdapterService } from '../../dates/CustomDateAdapterService';
import { DateAdapter } from '@angular/material/core';





@Component({
  selector: 'app-FileUpload',
  templateUrl: './FileUpload.component.html',
  styleUrls: ['./FileUpload.component.css']
})
export class FileUploadComponent implements OnInit {
  @Input() dataMedia: Media; //not used --> remove

  imageB64: string;
  private uploadSub: Subscription;
  imageInfos: Observable<any>;


  filesSelected: FileData[] = [];

  selectedFiles?: FileList;
  progress: any[] = [];
  message: string[] = [];
  previews: string[] = [];

  response: any;

  tittle: string;

  //mostrar data o no segun de donde sea llamado
  mostrarOpcionActivo = false;

  toastr2: ToastrService;

  myDate: Date;

  constructor(private appFileService: AppFileService,
    private dateAdapter: DateAdapter<Date>,
    private dataSer: CustomDateAdapterService
    //  private toastr: ToastrService
  ) {
    this.dateAdapter.setLocale('en-GB'); //dd/MM/yyyy

    this.myDate = this.dataSer.CalibrateDate(new Date());

  }

  ngOnInit() {
    //  this.document = new Document();
    this.mostrarOpcionActivo = false;
    this.tittle = "Upload Files"


  }
  Upload(idx: number, document: File) {
    //this.filesSelected[idx] ??
    this.progress[idx] = { value: 0, fileName: document.name };

    this.filesSelected[idx] = {
      dateUploaded: this.myDate,
      description: '',
      fileUploaded: false,
      name: document.name,
      size: 0,
      url: '',
      webUrl: '',
      id: undefined,
      urlPreview:'',

      documentType: //document.type
      {
        description: document.type,
        name: document.type,
        id:1
      }
        //document.type

    };

    if (document) {

      const formData: FormData = new FormData();

      formData.append('1', document);

      this.imageInfos = this.appFileService.UploadFile(formData)
        .pipe(
          finalize(() => {
            this.progress[idx] = 100;
            this.message.push('Uploaded the file successfully: ' + document.name);
            this.filesSelected[idx].fileUploaded = true;
            this.mostrarOpcionActivo = true;
            //this.filesSelected[idx].url=
          }),
          catchError(this.errorHandler)
        );

      this.uploadSub = this.imageInfos.subscribe((event: any) => {

        if (event.type === HttpEventType.UploadProgress) {
          this.progress[idx] = Math.round(100 * event.loaded / event.total);
          this.filesSelected[idx].size = event.total;

        }
        else if (event.type === HttpEventType.Response) {
          console.log(event.body.dbPath);
          this.filesSelected[idx].url = event.body.dbPath;
          this.appFileService.sendUrlDataFile(this.filesSelected[idx]);
        }
      })
    }



  }

  selectFiles(event: any): void {
    this.message = [];
    this.progress = [];
    this.selectedFiles = event.target.files;

    this.previews = [];
    if (this.selectedFiles && this.selectedFiles[0]) {

      const numberOfFiles = this.selectedFiles.length;
      for (let i = 0; i < numberOfFiles; i++) {
        const reader = new FileReader();

        reader.onload = (e: any) => {
          this.previews.push(e.target.result);
        };

        reader.readAsDataURL(this.selectedFiles[i]);
      }
    }
  }

  uploadFiles(): void {

    this.message = [];
    // const file: File = event.target.files;

    if (this.selectedFiles) {
      for (let i = 0; i < this.selectedFiles.length; i++) {
        this.Upload(i, this.selectedFiles[i]);

      }
    }

    return;


  }

  errorHandler(error: HttpErrorResponse) {
    console.log(error.message);
    return "";// Observable.throw(error.message || "server error.");
  }
  ngOnDestroy() {
    if (document) {
      this.uploadSub.unsubscribe();
    }
  }

}



