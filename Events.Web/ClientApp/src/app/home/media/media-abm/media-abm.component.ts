import { Component, inject, Injectable, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { Media } from 'src/app/model/media.model';
import { MatDatepicker } from '@angular/material/datepicker';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { first, Subscription } from 'rxjs';
import { AppMediaService } from '../../../server/app.media.service';
import { AppFileService } from '../../../server/app.file.service';
import { Events } from '../../../model/event.model';
import { AppService } from '../../../server/app.service';
import { TagItem } from '../../../model/tagItem.model';
import { FileData } from '../../../model/fileData.model';
import { NgImageSliderModule } from 'ng-image-slider';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-media-abm',
  templateUrl: './media-abm.component.html',
  styleUrls: ['./media-abm.component.css']
})
@Injectable({ providedIn: 'root' })
export class MediaAbmComponent implements OnInit, OnChanges, OnDestroy {

  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;
  @Input() mediaSelected: Media;
  @Input() abmMedia: boolean;

  mediaGroup: UntypedFormGroup;
  fb: UntypedFormBuilder;
  media: Media;
  buttonAction: string;
  selectedEvent: Events;
  mediaToSave: Media;
  tagItems: TagItem[] = [];

  filesToShow: FileData[];
  allFiles: FileData[];
  imageObject: object[]=[];


  private subscriptionFile: Subscription;
  private subscriptionEventFilter: Subscription;
  private subscriptionChipTags: Subscription;
  private subscriptionABMMedia: Subscription;


  constructor(fb: UntypedFormBuilder,
    private appMediaService: AppMediaService,
    private appFileService: AppFileService,
    private service: AppService,
    private route: ActivatedRoute) {
    this.fb = fb;


    
    this.subscriptionABMMedia = this.appMediaService.getUpdateMedia().subscribe
      (data => {
    ///    console.log("media abm ctor -- data: " + data);
        if (data != undefined) {

          if (data.data.abmObject == true) {
            this.mediaSelected = data.data.rowSelected;

            if (data.data.type != undefined) {
              this.abmMedia = true;
            }
          }
        }
      });

    this.subscriptionFile = this.appFileService.getUpdateFile().subscribe
      (data => {
        Array.prototype.push.apply(this.media.file, [data.data])

      });

    this.subscriptionEventFilter = this.service.getUpdateEvent().subscribe
      (data => {
        this.selectedEvent = data.data;
      });

    this.subscriptionChipTags = this.service.getUpdateChipTag().subscribe
      (data => {
        //     console.log("chip data: " + data.data);
        this.tagItems = data.data;
      })

  }


  ngOnInit(): void {
    console.log("media edit init: ");

    if (this.mediaSelected != undefined) {
      this.buttonAction = "Update";
      this.mediaGroup = this.CreateForm(this.mediaSelected);
    } else {
      this.buttonAction = "Add";
      this.mediaGroup = this.CreateForm(null);
    }

    this.media = {
      dateUploaded: new Date,
      description: '',
      name: '',
      id: -1,
      file: [],
      event: null,
      tagItems: [],
      onlyFilesInfo:[]

    }



  }

  getFileMedia() {
    this.filesToShow = this.mediaSelected.onlyFilesInfo;
 //   Array.prototype.push.apply(this.media.file, this.filesToShow)

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

  ngOnChanges(changes: SimpleChanges): void {
    if (this.mediaSelected == null) {

      //en teoria ya esta creada el form evento, tal vez lo uso si pongo crear despues de haber seleccionado a alguien
      this.mediaGroup = this.CreateForm(null);

    } else {
   
      this.media = this.mediaSelected;
    //  this.getFileMedia();

    }
  }

  //get media, events and tags
  CreateForm(mediaEdit: Media | null): UntypedFormGroup {
    if (mediaEdit == null) {
      
      return this.fb.group({
        Name: [null, [Validators.required]],
        Description: [null],
        DateUploaded: [null],

      });
    } else {
      this.selectedEvent = (mediaEdit.event == null) ? this.selectedEvent : mediaEdit.event;
      this.tagItems = (mediaEdit.tagItems == null) ? this.tagItems : mediaEdit.tagItems;

      
      this.getFileMedia();
      return this.fb.group({
        Name: new UntypedFormControl(mediaEdit.name ?? null),
        Description: new UntypedFormControl(mediaEdit.description ?? null),
        DateUploaded: new UntypedFormControl(mediaEdit.dateUploaded ?? null),

      });

    }

  }

  SaveMedia(MeidaABM: UntypedFormGroup) {
    if (MeidaABM.status == 'VALID') {
      this.mediaToSave = MeidaABM.value as Media;
    //  console.log('Current media: ', this.media);
      var newFiles = this.media.file as unknown as FileData[];
      var filesToSave = this.filesToShow as unknown as FileData[];
      //var ppp: FileData[]=[];
      //ppp.concat(adsfasf, asdfmamp);
      ////this.filesToShow.concat(, this.mediaToSave.file as unknown as FileData[]);
      
      if (filesToSave == undefined) {
        filesToSave = [];
      }
      for (var i = 0; i < newFiles.length; i++) {
        
        filesToSave.push(newFiles[i])
      }
      this.mediaToSave.file = filesToSave;

    //  Array.prototype.push.apply(this.mediaToSave.file, [this.filesToShow]);



      this.mediaToSave.event = this.selectedEvent;
      this.mediaToSave.tagItems = this.tagItems;
      this.mediaToSave.description = this.mediaToSave.description != null ? this.mediaToSave.description:((this.selectedEvent == null) ? '' : this.selectedEvent.description);

      if (this.buttonAction == "Update") {
        this.mediaToSave.id = this.mediaSelected.id;
        this.appMediaService.UpdateMedia(this.mediaSelected.id, this.mediaToSave)
          .pipe(first())
          .subscribe(
            data => {
              console.log('Current data update: ', data);
              this.ABMMediaFinished();
            },
            error => console.log('Error Getting Position: ', error)
          );
      }
      else {

        this.appMediaService.AddMedia(this.mediaToSave).pipe(first())
          .subscribe(
            data => {
              console.log('Current data create: ', data);
              this.ABMMediaFinished();

            },
            error => console.log('Error Getting Position: ', error)
          );
      }

    }
  }
  ngOnDestroy() {
    this.subscriptionFile.unsubscribe();
    this.subscriptionEventFilter.unsubscribe();
    this.subscriptionChipTags.unsubscribe();
    this.subscriptionABMMedia.unsubscribe();
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
