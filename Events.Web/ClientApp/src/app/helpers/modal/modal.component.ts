import { Component, Input } from "@angular/core";

@Component({
    selector: 'modal',
    templateUrl: './modal.component.html'
  })

  //TODO: Find the way to do it generic
  
  export class Modal {
  
    @Input() tittle: string;
    @Input() text: string;
  
   // constructor(public activeModal: ) { }
}