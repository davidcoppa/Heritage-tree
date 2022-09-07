import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit,  Component, Input, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { AppService } from 'src/app/server/app.service';
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
export class CountrylistComponent implements AfterViewInit {
  displayedColumns = ['Name',
    'Code',
    'Capital',
    'Region',
    'Latitude',
    'Longitude',
    'FullName',
    'State',
    'Action'
  ];

  stateDisplaedColumns = ['Name',
    'Code',
    'Capital',
    'Region',
    'Latitude',
    'Longitude',
    'FullName',
    'City'
  ];

  cityDisplaedColumns = ['Name',
    'Code',
    'Capital',
    'Region',
    'Latitude',
    'Longitude',
    'FullName'
  ];

  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  @ViewChildren('innerTablesState') innerTablesState: QueryList<MatTable<Object>>;
  @ViewChildren('innerSortState' ) innerSortState: QueryList<MatSort>;

  @ViewChildren('innerTablesCity') innerTablesCity: QueryList<MatTable<Object>>;
  @ViewChildren('innerSortCity') innerSortCity: QueryList<MatSort>;

  @Input() dataCountry: Country[];
  resultsLength = 0;
  listModel: ListObject;
  listSubObject: ListSubObject;

  expandedElement: Country | null;
  expandedElementCity: State | null;

  valueSelectedState: State;
  valueSelectedCity: City;


  constructor(private service: AppService,
  ) { }

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

    (elementCity.cities != [] && (elementCity.cities as MatTableDataSource<City>).data?.length) ?
      (this.expandedElementCity = this.expandedElementCity === elementCity ? null : elementCity) : null;

  }


  toggleRowState(element: object) {
    console.log("state list click");

    var elementCountry = element as Country;

    (elementCountry.state != [] && (elementCountry.state as MatTableDataSource<State>).data?.length) ?
      (this.expandedElement = this.expandedElement === elementCountry ? null : elementCountry) : null;
  }

  applyFilter(filterValue: string) {
    this.innerTablesState.forEach((table, index) => (table.dataSource as MatTableDataSource<State>).filter = filterValue.trim().toLowerCase());
  }




  editCountry(contact: Country) {
    this.listModel.abmObject = true;
    this.listModel.rowSelected = contact;

    this.service.sendUpdateCountry(this.listModel);


  }

  viewCountry(contact: Country) {
    // let route = '/contacts/view-contact';
    // this.router.navigate([route], { queryParams: { id: contact.id } });
  }

}
