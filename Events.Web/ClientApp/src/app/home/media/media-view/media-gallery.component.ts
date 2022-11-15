import { Component, OnChanges } from '@angular/core';
import { AppMediaService } from '../../../server/app.media.service';

@Component({
  selector: 'app-media-gallery',
  templateUrl: './media-gallery.component.html',
  styleUrls: ['./media-gallery.component.css']
})

export class GalleryComponent implements OnChanges {
  images: any[];
  filterBy?: string = 'all'
  allImages: any[] = [];

  constructor(private appMediaService: AppMediaService) {
    this.allImages = this.appMediaService.getImages();
  }
  ngOnChanges() {
    this.allImages = this.appMediaService.getImages();
  }
}   
