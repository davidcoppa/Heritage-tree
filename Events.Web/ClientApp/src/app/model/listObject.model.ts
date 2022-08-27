import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

export class ListObject {
  sort: MatSort;
  paginator: MatPaginator;
  abmObject: boolean = false;
  rowSelected: Object;
}
