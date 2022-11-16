import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { Media } from 'src/app/model/media.model';
import { MatDatepicker } from '@angular/material/datepicker';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs';
import { AppMediaService } from '../../../server/app.media.service';


@Component({
  selector: 'app-media-abm',
  templateUrl: './media-abm.component.html',
  styleUrls: ['./media-abm.component.css']
})
export class MediaAbmComponent implements OnInit, OnChanges {
  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;
  @Input() mediaSelected: Media;
  @Input() abmMedia: boolean;


  mediaGroup: FormGroup;
  fb: FormBuilder;
  media: Media;
  buttonAction: string = "Add";

  constructor(fb: FormBuilder, private appMediaService: AppMediaService) {
    this.fb = fb;
  }


  ngOnInit(): void {
    this.mediaGroup = this.CreateForm(null);
  }


  ngOnChanges(changes: SimpleChanges): void {
    if (this.mediaSelected == null) {

      //en teoria ya esta creada el form evento, tal vez lo uso si pongo crear despues de haber seleccionado a alguien
      this.mediaGroup = this.CreateForm(null);

    } else {
      this.media = this.mediaSelected;

    }
  }
CreateForm(mediaEdit:Media| null): FormGroup {
    if(mediaEdit==null)
    {
      return this.fb.group({
        Name: [null, [Validators.required]],
        Description: [null],
        MediaDate: [null],
        MediaDateUploaded:[null],
        MediaType: [null],
        UrlFile: [null]
      });
    }else{
      return this.fb.group({
        Name: new FormControl(mediaEdit.mediaName ?? null),
        Description: new FormControl(mediaEdit.description ?? null),
        MediaDate:new FormControl(mediaEdit.mediaDate ?? null),
        MediaDateUploaded:new FormControl(mediaEdit.mediaDateUploaded ?? null),
        MediaType: new FormControl(mediaEdit.mediaType ?? null),
        UrlFile: new FormControl(mediaEdit.urlFile ?? null)
        
      });    }
  
  }

  SaveMedia(MeidaABM: FormGroup) {
    this.media = MeidaABM.value as Media;

    if (this.buttonAction == "Update") {
      this.appMediaService.UpdateMedia(this.mediaSelected.id, this.media)
        .pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMMediaFinished();
          },
          error => console.log('Error Getting Position: ', error)
        );
    }
    else {

      this.appMediaService.AddMedia(this.media).pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMMediaFinished();

          },
          error => console.log('Error Getting Position: ', error)
        );
    }


  }

  ABMMediaFinished() {
    this.appMediaService.sendUpdateMedia(true);
  }

  ResetForm() {
    this.mediaGroup = this.CreateForm(null);
  }

  Cancel() {
    this.appMediaService.sendUpdateMedia(true);
  }

}
