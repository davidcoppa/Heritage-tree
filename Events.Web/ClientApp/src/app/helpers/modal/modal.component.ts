import { Component, Input, OnInit } from "@angular/core";

@Component({
    selector: 'modal',
    templateUrl: './modal.component.html'
  })

  //TODO: Find the way to do it generic

export class Modal implements OnInit {
  
    //@Input() tittle: string;
    //@Input() text: string;
  
  @Input() tittle: string | undefined;
  @Input() text: string | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
