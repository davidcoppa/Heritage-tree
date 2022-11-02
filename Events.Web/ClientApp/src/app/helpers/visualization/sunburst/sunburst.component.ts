import { AfterViewInit, Component, Input, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { first } from 'rxjs';
import { Person } from '../../../model/person.model';
import { PersonWithParents } from '../../../model/PersonWithParents.model';
import { AppService } from '../../../server/app.service';

import * as dTree from 'd3-dtree';

//declare var dTree1: any;
//declare function GetTree(data:any): any;
function tree(data: any, options: any) {

  //treeJson =
    d3.json(data, function (error: any, treeData: any) {
   dTree.default.init(data,
    {
      target: "#graph",
      debug: true,
      hideMarriageNodes: false,
      marriageNodeSize: 5,
      height: 800,
      width: 1200,
      callbacks: {
        nodeClick: function (name: any, extra: any) {
          alert('Click: ' + name);
        },
        nodeRightClick: function (name: any, extra: any) {
          alert('Right-click: ' + name);
        },
        textRenderer: function (name: any, extra: any, textClass: any) {
          if (extra && extra.nickname)
            name = name + " (" + extra.nickname + ")";
          return "<p align='center' class='" + textClass + "'>" + name + "</p>";
        },
        marriageClick: function (extra: any, id: any) {
          alert('Clicked marriage node' + id);
        },
        marriageRightClick: function (extra: any, id: any) {
          alert('Right-clicked marriage node' + id);
        },
      }
     });
  });
}

@Component({
  selector: 'app-Sunburst',
  templateUrl: './sunburst.component.html',
  styleUrls: ['./sunburst.component.css'],
})
export class SunburstComponent implements AfterViewInit {

  dataChart: PersonWithParents;

  @Input() dataPersonToShow: Person;
  dataReturned = dTree;

  constructor(private service: AppService,
  ) {
    // this.dataReturned = datas;
  }

  ngAfterViewInit() {

    if (this.dataPersonToShow != undefined) {
      this.GetData()
    } else {
      console.log("no person selected.")
    }

  }



  GetData() {
    this.service.GetDataToVisualize(this.dataPersonToShow.id)
      .pipe(first())
      .subscribe(
        data => {
    //      console.log("data: " + data);
      //    this.dataReturned = datas;

          //   dTree.init(datas, options)


        //  (function ($:any) {
            //$(document).ready(function () {
            //  console.log("Hello from jQuery!");
            //  tree(data, options);
            //});
         tree(datas, options);
        //  });
          //(dTree)
          //    var treeData = data;// dTree.init(data, options);

          //  this.myChart
          //    .data(data)
          ////    .size('value')
          ////    .color('red')
          // //   .radiusScaleExponent(1)
          //    (document.getElementById('chart') ?? new HTMLElement());
        },
        error => console.log('Error Getting Position: ', error)
      );
  }


}
//npm install d3-dtree
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
  marriageNodeSize: 10,
  callbacks: {
    /*
      Callbacks should only be overwritten on a need to basis.
      See the section about callbacks below.
    */
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



