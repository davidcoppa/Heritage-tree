import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { Person } from './person.model';

export class ListObject {

  sort: MatSort;
  paginator: MatPaginator;


  abmperson: boolean = false;
  rowSelected: Object;


  //resultsLength: number;
  //pageSize: number;

  //dataResult: object;
}
