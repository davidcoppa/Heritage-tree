import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { AppMediaService } from '../../../../server/app.media.service';

@Component({
  selector: 'app-media-details',
  templateUrl: './media-details.component.html',
  styleUrls: ['./media-details.component.css']
})
export class MediaDetailsComponent {
  image: any

  constructor(private appMediaService: AppMediaService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.image = this.appMediaService.getImage(
      this.route.snapshot.params['id']
    )
  }
}    
