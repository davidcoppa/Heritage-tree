import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { Media } from 'src/app/model/media.model';
import { MatDatepicker } from '@angular/material/datepicker';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
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


  mediaGroup: UntypedFormGroup;
  fb: UntypedFormBuilder;
  media: Media;
  buttonAction: string = "Add";

  constructor(fb: UntypedFormBuilder, private appMediaService: AppMediaService) {
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
CreateForm(mediaEdit:Media| null): UntypedFormGroup {
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
        Name: new UntypedFormControl(mediaEdit.mediaName ?? null),
        Description: new UntypedFormControl(mediaEdit.description ?? null),
        MediaDate:new UntypedFormControl(mediaEdit.mediaDate ?? null),
        MediaDateUploaded:new UntypedFormControl(mediaEdit.mediaDateUploaded ?? null),
        MediaType: new UntypedFormControl(mediaEdit.mediaType ?? null),
        UrlFile: new UntypedFormControl(mediaEdit.urlFile ?? null)
        
      });    }
  
  }

  SaveMedia(MeidaABM: UntypedFormGroup) {
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
