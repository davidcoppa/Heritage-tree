import { AfterViewInit, Component, Input, NgZone, OnInit, HostListener, ViewChild, ViewChildren } from '@angular/core';
import { first } from 'rxjs';
import { Person } from '../../../model/person.model';
import { PersonWithParents } from '../../../model/PersonWithParents.model';
import { AppService } from '../../../server/app.service';

import * as dTree from '../../../../assets/Js/dtree';


function tree(data: any, options: any) {



  dTree.default.init(data, options);

}
function callAngularFunction(name: any, extra: any, objectId: any) {

  var event = new CustomEvent("get.tree",
    {
      detail: objectId,
      bubbles: true,
      cancelable: true
    }
  );
  window.dispatchEvent(event);

  
}

@Component({
  selector: 'app-Sunburst',
  templateUrl: './sunburst.component.html',
  styleUrls: ['./sunburst.component.css'],
})
export class SunburstComponent implements AfterViewInit, OnInit{

  dataChart: PersonWithParents;

  @Input() dataPersonToShow: Person;
  dataReturned = dTree;

  
  constructor(private service: AppService,
  ) {
   
  }

  @HostListener("window:get.tree", ['$event'])
  onPaymentSuccess(event: any): void {
   // alert('Angular 2+ function is called: ' + event.detail);
    this.GetData(event.detail);

  }


  ngOnInit() {
    
  }

  ngAfterViewInit() {

    if (this.dataPersonToShow != undefined) {
      this.GetData(this.dataPersonToShow.id);
    } else {
      console.log("no person selected.")
    }

  }


  GetData(personTofind: number) {
    this.service.GetDataToVisualize(personTofind)
      .pipe(first())
      .subscribe(
        data => {
          //  console.log("data: " + JSON.stringify(data));
          tree(data, options);
        },
        error => console.log('Error Getting Position: ', error)
      );
  }


}

const datas =
  [{
    "name": "Niclas Superlongsurname",
    "class": "man",
    "textClass": "emphasis",
    "marriages": [{
      "spouse": {
        "name": "Iliana",
        "class": "woman",
        "extra": {
          "nickname": "Illi"
        }
      },
      "children": [{
        "name": "James",
        "class": "man",
        "marriages": [{
          "spouse": {
            "name": "Alexandra",
            "class": "woman"
          },
          "children": [{
            "name": "Eric",
            "class": "man",
            "marriages": [{
              "spouse": {
                "name": "Eva",
                "class": "woman"
              }
            }]
          }, {
            "name": "Jane",
            "class": "woman"
          }, {
            "name": "Jasper",
            "class": "man"
          }, {
            "name": "Emma",
            "class": "woman"
          }, {
            "name": "Julia",
            "class": "woman"
          }, {
            "name": "Jessica",
            "class": "woman"
          }]
        }]
      }]
    }]
  }];





const options =
{
  target: '#graph',
  debug: false,
  width: 600,
  height: 600,
  hideMarriageNodes: true,
  marriageNodeSize: 1,
  callbacks: {
    nodeClick: function (name: any, extra: any, id: any, objectId: any) {
    //  alert('Click objectId: ' + objectId);
      if (objectId!=0) {
        callAngularFunction(name, extra, objectId);
      }
    },
    nodeRightClick: function (name: any, extra: any) {
      alert('Right-click: ' + name);
    },
    textRenderer: function (name: any, extra: any, textClass: any) {
      if (extra && extra.nickname)
        name = name + " (" + extra.nickname + ")";
      return "<p align='center' class='" + textClass + "'>" + name + "</p>";
    },
    marriageClick: function (name: any, extra: any, id: any, objectId: any) {

    //  alert('Clicked marriage node' + objectId);


      if (objectId != 0) {
        callAngularFunction(name, extra, objectId);
      }


    },
    marriageRightClick: function (extra: any, id: any) {
      alert('Right-clicked marriage node' + id);
    },
  },
  margin: {
    top: 0,
    right: 0,
    bottom: 0,
    left: 0
  },
  nodeWidth: 100,
  styles: {
    node: 'node',
    linage: 'linage',
    marriage: 'marriage',
    text: 'nodeText'
  }
}



