import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Document } from '../../../Model/Document';
import { ServerService } from '../../../service/server.service';


@Component({
  selector: 'app-downloadFile',
  templateUrl: './DownloadFile.component.html',
  styleUrls: ['./DownloadFile.component.css']
})
export class DownloadFileComponent implements OnInit {
  progress: number;
  message: string;
  blockButton: boolean;
  // fileUrl: "GITS - Brochure";

  constructor(
    private server: ServerService,
    //private document: Document
  ) { }

  ngOnInit(): void {
  }

  download() {

    //console.log('descargfandp');
    if (this.blockButton) {
      return false;
    }
    this.blockButton = true;
    this.server.GetLastNewsletter().subscribe((event) => {
      //console.log('ddasd');
      if (event.type === HttpEventType.UploadProgress)

        this.progress = Math.round((100 * event.loaded) / event.total);
      else if (event.type === HttpEventType.Response) {
     //   console.log('Download');

        //   this.message = 'Download success.';
        this.downloadFile(event);

      }

      //let blob: any = new Blob([event.blob()], { type: 'text/json; charset=utf-8' });
      //const url = window.URL.createObjectURL(blob);
      //window.location.href = url;//response.url

      //  window.open(url);
    }), (error: any) => {
     // console.log('Error downloading the file')
      this.blockButton = false;

    };

    return false;



    //   ((response: any) => {

    //   //  document = response.mensaje;
    //   console.log(response);
    // //  let blob: any = new Blob([response.blob()], { type: 'text/json; charset=utf-8' });
    // //  const url = window.URL.createObjectURL(blob);
    // //  window.location.href = url;//response.url

    ////   window.open(url);

    // }), (error: any) => console.log('Error downloading the file');




  }
  private downloadFile(data: HttpResponse<Blob>) {
    if (data.body != null) {
    //  console.log('data is NOT null');

      const downloadedFile = new Blob([data.body], { type: data.body.type });

      const a = document.createElement('a');
      a.setAttribute('style', 'display:none;');
      document.body.appendChild(a);
      a.download = "GITS - Brochure.pdf";
      a.href = URL.createObjectURL(downloadedFile);
      a.target = '_blank';
      a.click();
      document.body.removeChild(a);
    }
    else {
  //    console.log('data is null');
    }
    this.blockButton = false;

  }



}
