import { Component, ContentChild, ViewChild, OnInit} from '@angular/core';
import { MatFormField, MatFormFieldControl } from '@angular/material/form-field';


@Component({
  selector: 'app-mat-field',
  template: `<mat-form-field>
  <ng-content></ng-content>
  <div>
    <!-- Some warning messages --> 
  </div>
</mat-form-field>`
})
export class FieldComponent implements OnInit { 
    @ContentChild(MatFormFieldControl) _control: MatFormFieldControl<any>;
    @ViewChild(MatFormField) _matFormField: MatFormField;

    ngOnInit() {
        this._matFormField._control = this._control;
    }
}