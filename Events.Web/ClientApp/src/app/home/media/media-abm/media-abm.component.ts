import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { Media } from 'src/app/model/media.model';
import { MatDatepicker } from '@angular/material/datepicker';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AppService } from 'src/app/server/app.service';
import { first } from 'rxjs';


@Component({
  selector: 'app-media-abm',
  templateUrl: './media-abm.component.html',
  styleUrls: ['./media-abm.component.css']
})
export class MediaAbmComponent implements OnInit, OnChanges {
  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;
  @Input() mediaSelected: Media;

  mediaGroup: FormGroup;
  fb: FormBuilder;
  media: Media;
  constructor(fb: FormBuilder, private appService: AppService) {
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
        Name: new FormControl(mediaEdit.Name ?? null),
        Description: new FormControl(mediaEdit.Description ?? null),
        MediaDate:new FormControl(mediaEdit.MediaDate ?? null),
        MediaDateUploaded:new FormControl(mediaEdit.MediaDateUploaded ?? null),
        MediaType: new FormControl(mediaEdit.MediaType ?? null),
        UrlFile: new FormControl(mediaEdit.UrlFile ?? null)
        
      });    }
  
  }

  SaveMedia(MeidaABM: FormGroup) {
    this.media = MeidaABM.value as Media;

    this.appService.AddMedia(this.media).pipe(first())
      .subscribe(
        {
          next(data) {
            console.log('Current Position: ', data);
          },
          error(msg) {
            console.log('Error Getting Location: ', msg);
          }
        }
      );
  }

}
