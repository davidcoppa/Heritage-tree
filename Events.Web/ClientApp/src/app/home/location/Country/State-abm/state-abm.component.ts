import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first, Subscription } from 'rxjs';
import { AppService } from 'src/app/server/app.service';
import { ListObject } from '../../../../model/listObject.model';
import { State } from '../../../../model/state.model';

@Component({
  selector: 'app-state-abm',
  templateUrl: './state-abm.component.html'
})
export class StateAbmComponent implements OnInit, OnDestroy {
  @Input() stateSelected: State;
  @Input() abmState: boolean;

  state: FormGroup;
  fb: FormBuilder;
  evt: State;
  buttonAction: string;

  listModel: ListObject;

  private subscriptionStateFilter: Subscription;

  constructor(fb: FormBuilder, private service: AppService) {
    this.fb = fb;

    this.subscriptionStateFilter = this.service.getUpdateState().subscribe
      (data => { 
        console.log("sendUpdate state: " + data.data);
        this.stateSelected = data.data;
      });
  }

  ngOnDestroy() {
    this.subscriptionStateFilter.unsubscribe();
  }

  ngOnInit(): void {
    if (this.stateSelected != undefined) {
      this.state = this.CreateForm(this.stateSelected);
    } else {
      this.state = this.CreateForm(null);
    }
  }

  CreateForm(stateEdit: State | null): FormGroup {

    if (stateEdit == null) {

      this.buttonAction = "Add";

      return this.fb.group({
        name: [null, [Validators.required]],
        code: [null],
        capital: [null],
        region: [null],
        latitude: [null],
        longitude: [null]
      });
    } else {
      this.evt = this.stateSelected;
      this.buttonAction = "Update";

      return this.fb.group({
        name: new FormControl(stateEdit.name ?? null),
        code: new FormControl(stateEdit.code ?? null),
        capital: new FormControl(stateEdit.capital ?? null),
        region: new FormControl(stateEdit.region ?? null),
        latitude: new FormControl(stateEdit.lat ?? null),
        longitude: new FormControl(stateEdit.lgn ?? null)
      });
    }

  }

  ngOnChanges() {
    if (this.stateSelected == null) {
      this.state = this.CreateForm(null);
    } else {
      this.evt = this.stateSelected;
    }
  }

  saveState(stateABM: FormGroup) {
    this.evt = stateABM.value as State;

    console.log('Current State abm: ', this.evt);

    if (this.buttonAction == "Update") {

      this.evt.id = (this.stateSelected).id;

      this.service.UpdateStates((this.stateSelected).id, this.evt)
        .pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMStateFinished();
          },
          error => console.log('Error Getting Position: ', error)
        );
    }
    else {

      this.service.AddStates(this.evt).pipe(first())
        .subscribe(
          data => {
            console.log('Current data: ', data);
            this.ABMStateFinished();

          },
          error => console.log('Error Getting Position: ', error)
        );
    }
  }

  ABMStateFinished() {
    this.service.sendUpdateState(true);
  }

  ResetForm() {
    this.state = this.CreateForm(null);
  }

  Cancel() {
    this.service.sendUpdateState(true);
  }

}


