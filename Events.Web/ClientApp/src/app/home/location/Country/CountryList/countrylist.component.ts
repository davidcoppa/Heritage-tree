import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, Input, OnDestroy, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { AppService } from 'src/app/server/app.service';
import { LocationEnum } from '../../../../helpers/enums/location.enum';
import { City } from '../../../../model/city.model';
import { Country } from '../../../../model/country.model';
import { ListObject } from '../../../../model/listObject.model';
import { ListSubObject } from '../../../../model/listSubObject';
import { State } from '../../../../model/state.model';

@Component({
  selector: 'app-countrylist',
  templateUrl: './countrylist.component.html',
  styleUrls: ['./countrylist.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],

})
export class CountrylistComponent implements AfterViewInit, OnDestroy {
  displayedColumns = ['Name',
    'Code',
    'Capital',
    'Region',
    'Coordinates',
    'FullName',
    'State',
    'Action'
  ];

  stateDisplaedColumns = ['Name',
    'Code',
    'Capital',
    'Region',
    'Coordinates',
    'FullName',
    'City',
    'Action'
  ];

  cityDisplaedColumns = ['Name',
    'Code',
    'Capital',
    'Region',
    'Coordinates',
    'FullName',
    'Action'
  ];

  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  @ViewChildren('innerTablesState') innerTablesState: QueryList<MatTable<Object>>;
  @ViewChildren('innerSortState') innerSortState: QueryList<MatSort>;

  @ViewChildren('innerTablesCity') innerTablesCity: QueryList<MatTable<Object>>;
  @ViewChildren('innerSortCity') innerSortCity: QueryList<MatSort>;

  @Input() dataCountry: Country[];
  @Input() abmObject: boolean;

  resultsLength = 0;
  listModel: ListObject;
  listSubObject: ListSubObject;

  expandedElement: Country | null;
  expandedElementCity: State | null;

  valueSelectedState: State;
  valueSelectedCity: City;


  //private subscriptionNewStateCountry: Subscription;


  constructor(private service: AppService,
  ) {
    //getAddStateToCountry
    //this.subscriptionNewStateCountry = this.service.getAddStateToCountry().subscribe
    //  (data => {
    //    if (data != undefined) {
    //      if (data.data != undefined) {
    //        if (data.data.abmObject == true) {
    //          this.abmObject = data.data.abmObject;
    //        }
    //      }
    //    }
    //  });



  }
  ngOnDestroy(): void {
  //  this.subscriptionNewStateCountry.unsubscribe();

  }

  ngAfterViewInit() {


    console.log("ctor country list");

    this.listModel = new ListObject();
    this.sort.direction = "desc";
    this.sort.active = "Name";
    this.sort.disableClear;
    this.paginator = this.paginator;

    this.listModel.sort = this.sort;
    this.listModel.paginator = this.paginator;

    this.service.sendUpdateCountry(this.listModel);
  }

  toggleRowCity(element: object) {
    console.log("City list click - element: " + element);

    var elementCity = element as State;

    (elementCity.cities && (elementCity.cities as MatTableDataSource<City>).data?.length) ?
      (this.expandedElementCity = this.expandedElementCity === elementCity ? null : elementCity) : null;

  }


  toggleRowState(element: object) {
    console.log("state list click");

    var elementCountry = element as Country;

    (elementCountry.state && (elementCountry.state as MatTableDataSource<State>).data?.length) ?
      (this.expandedElement = this.expandedElement === elementCountry ? null : elementCountry) : null;
  }
  //(elementCity.file as MatTableDataSource<FileData>)

  applyFilter(filterValue: string) {
    this.innerTablesState.forEach((table, index) => (table.dataSource as MatTableDataSource<State>).filter = filterValue.trim().toLowerCase());
  }


  viewCountry(contact: Country) {
    //idea: open on maps
  }
  viewState(contact: State) {
    //idea: open on maps
  }
  viewCity(contact: City) {
    //idea: open on maps
  }

  editCountry(contact: Country) {

    this.abmObject = true;
    this.listModel.abmObject = true;
    this.listModel.rowSelected = contact;
    this.listModel.type = LocationEnum.country;
    // this.service.sendUpdateCountry();
    this.service.sendUpdateABMLocation(this.listModel);
  }

  editState(contact: State) {
    this.abmObject = true;

    this.listModel.abmObject = true;
    this.listModel.rowSelected = contact;
    this.listModel.type = LocationEnum.state;
    this.service.sendUpdateABMLocation(this.listModel);
  }

  editCity(contact: City) {
    this.abmObject = true;

    this.listModel.abmObject = true;
    this.listModel.rowSelected = contact;
    this.listModel.type = LocationEnum.city;
    this.service.sendUpdateABMLocation(this.listModel);
  }



}
