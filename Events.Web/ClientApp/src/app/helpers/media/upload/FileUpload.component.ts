import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component, Input, EventEmitter, Output } from '@angular/core';
import { Observable, Subject, Subscription } from 'rxjs';
import { catchError, finalize, map } from 'rxjs/operators';
import { OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AppFileService } from '../../../server/app.file.service';
import { Media } from '../../../model/media.model';


@Component({
  selector: 'app-FileUpload',
  templateUrl: './FileUpload.component.html',
  styleUrls: ['./FileUpload.component.css']
})
export class FileUploadComponent implements OnInit {
  @Input() dataMedia: Media;

  imageB64: string;
  private uploadSub: Subscription;
  private upload$: Observable<any>;

  requiredFileType: '.pdf,.doc,.docx'; //add image types

  fileName = '';
  FileUploaded = false;

  progress: number;
  message: string;

  FileUrl: "";
  response: any;

  document: Media;
  active = true;
  tittle: string;

  //mostrar data o no segun de donde sea llamado
  mostrarOpcionActivo = false;


  constructor(private appFileService: AppFileService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
  //  this.document = new Document();
    this.mostrarOpcionActivo = false;
    this.tittle = "Upload a newsletter"


  }
  Upload(data: FormData) {

    this.upload$ = this.appFileService.UploadFile(data).pipe(

      finalize(() => {
        this.progress = 100;
        this.message = 'Upload Complete';
        this.FileUploaded = true;
        this.active = true;
        this.mostrarOpcionActivo = true;
      }),

      catchError(this.errorHandler)

    );

    this.uploadSub = this.upload$.subscribe((event: any) => {

      if (event.type === HttpEventType.UploadProgress) {
        this.progress = Math.round(100 * event.loaded / event.total);
        this.document.size = event.total;

      }
      else if (event.type === HttpEventType.Response) {
        console.log(event.body.dbPath);
        this.document.urlFile = event.body.dbPath;
        this.appFileService.sendUrlDataFile(this.document.urlFile);
      }
    })
  }

  //SaveB64() {
  //  console.log(this.imageB64);
  //  if (this.imageB64 != undefined) {
  //    const formData = new FormData();

  //    let blob = new Blob([this.imageB64], { type: 'image/png' });
  //    let file = new File([blob], "image.jpg");

  //    formData.append("3", file);//image_data
  //    //this.Upload(formData);

  //    this.Upload(formData);
  //  }

  //}



  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.fileName = file.name
      this.document.mediaName = this.fileName;
   //   this.document.documentType = this.fileName.split('.').pop();


      const formData = new FormData();

      //Change: this.document.mediaName for something more usefull
      formData.append(this.document.mediaName, file, this.fileName);

      this.Upload(formData);
      this.mostrarOpcionActivo = true;
    }
  }

  errorHandler(error: HttpErrorResponse) {
    console.log(error.message);
    return "";// Observable.throw(error.message || "server error.");
  }

  Save() {
    if (this.document.urlFile == undefined) {
      return;
    }
    const upload$ = this.appFileService.SendFile(this.document).subscribe(res => {
      this.toastr.success("File Saved", "=)");

      this.appFileService.sendUpdateFile();

      this.reset();

      return;
    },
      (err) => {

        console.log(err);

      });

  }
  cancelUpload() {
    if (this.uploadSub != null) {
      this.uploadSub.unsubscribe();

    }
    this.FileUploaded = false;

    this.reset();
  }

  reset() {
    this.progress = 0;
     this.message = '';
    this.FileUploaded = true;
    this.active = false;
  }

}



