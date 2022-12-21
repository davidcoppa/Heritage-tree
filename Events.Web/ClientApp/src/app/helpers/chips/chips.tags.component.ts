import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { first, map, startWith } from 'rxjs/operators';
import { TagItem } from '../../model/tagItem.model';
import { AppService } from '../../server/app.service';


@Component({
  selector: 'app-chips-autocomplete',
  templateUrl: 'chips.tags.component.html',
  styleUrls: ['chips.tags.component.css'],
})
export class ChipsTags implements OnInit {
  separatorKeysCodes: number[] = [ENTER, COMMA];
  @Input() dataTags: TagItem;


  dataControler = new UntypedFormControl('');
  filteredObjects: Observable<TagItem[]>;

  // allItems: string[] ;
  allItems: TagItem[];
  itemTags: TagItem[]=[];

  @ViewChild('dataInput') dataInput: ElementRef<HTMLInputElement>;

  constructor(private service: AppService) {

  }
  ngOnInit(): void {
    this.GetItems()
  }

  GetItems() {

    //get tags
    console.log('event list');

    this.service.GetAllTags('Name', 'desc', 0, 10000, '')
      .pipe(first())
      .subscribe(
        data => {
          //  console.log('Current data filter event type: ', data);

          this.allItems = data;

          this.filteredObjects = this.dataControler.valueChanges.pipe(
            startWith(''),
            map(value => {
              const name = typeof value === 'string' ? value : value?.name;
              return name ? this._filter(name as string) : this.allItems.slice();
              //return name ? this._filter(name as string) : this.allItems.slice();
            })
          );

        },
        error => console.log('Error Getting Position: ', error)
      );



  }

  add(event: MatChipInputEvent): void {


    const value = (event.value).trim();

    var jump = false;
    this.allItems.forEach((element, index) => {
      if (element.name == value)
        //DO nothing?
        jump = true;
      //this.allItems.splice(index, 1);
    });

    if (!jump) {
      var addVal: TagItem = { name: value, id: -1, isNew: true }


      // Add our fruit
      if (value) {
        Array.prototype.push.apply(this.itemTags, [addVal])
        //  this.itemOnList.push(addVal);
        this.service.sendUpdateChipTag(this.itemTags);
      }
    }


    // Clear the input value
    event.chipInput!.clear();

    this.dataControler.setValue(null);
  }

  remove(fruit: TagItem): void {
    if (this.itemTags == undefined) {
      return;
    }

    this.itemTags.forEach((element, index) => {
      if (element.name == fruit.name) {
        this.itemTags.splice(index, 1);
        this.service.sendUpdateChipTag(this.itemTags);
      }
    });

  }

  selected(event: MatAutocompleteSelectedEvent): void {

    var value = event.option.value;
    var jump = false;

    if (this.itemTags == undefined) {
      this.allItems.forEach(listItem => {
        if (listItem.name == value.name) {
          this.itemTags.push(listItem);
          //Array.prototype.push.apply(this.itemTags, [listItem])

        }
      })
    }
    this.itemTags.forEach((element, index) => {
      if (element.name == value.name)
        //DO nothing?
        jump = true;
      //this.allItems.splice(index, 1);
    });

    if (!jump) {
      this.itemTags.push(event.option.value);
      this.dataInput.nativeElement.value = '';
      this.dataControler.setValue(null);
      this.service.sendUpdateChipTag(this.itemTags);

    }



  }


  displayTagItem = (tags: TagItem): string => {

    if (this.dataTags != undefined) {
      if (tags.id != undefined) {
        this.dataTags = tags;
        this.service.sendUpdateChipTag(this.dataTags);
      } else {
        this.dataControler.setValue(this.dataTags);
      }
    }

    this.service.sendUpdateChipTag(tags);

    return tags && tags.name ? tags.name : '';
  }


  private _filter(value: string): TagItem[] {
    const filterValue = value.toLowerCase();


    return this.allItems.filter(fruit => fruit.name.toLowerCase().includes(filterValue));
  }
}
