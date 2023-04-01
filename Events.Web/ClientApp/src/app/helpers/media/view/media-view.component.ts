import { Component, Injectable, Input, OnInit } from '@angular/core';
import { Media } from 'src/app/model/media.model';
import { FileData } from '../../../model/fileData.model';


@Component({
  selector: 'app-media-view',
  templateUrl: './media-view.component.html',
  styleUrls: ['./media-view.component.css']
})
@Injectable({ providedIn: 'root' })
export class MediaAbmComponent implements OnInit {

  @Input() mediaSelected: Media;
  @Input() abmMedia: boolean;

  filesToShow: FileData[];
  imageObject: object[]=[];

  constructor(
  ) {


  }


  ngOnInit(): void {
    this.getFileMedia();
  }

  getFileMedia() {
    this.filesToShow = this.mediaSelected.onlyFilesInfo;

    for (var i = 0; i < this.filesToShow.length; i++) {
   //   console.log("media this.filesToShow  i : " + this.filesToShow[i]);

      var obj = {
        image: this.filesToShow[i].url,
        thumbImage: this.filesToShow[i].urlPreview,
        alt: this.filesToShow[i].description,
        title: this.filesToShow[i].name
      }


      this.imageObject.push(obj);

    }
  }

  imageClickHandler(e:any) {
    console.log('image click', e);
  }


}
