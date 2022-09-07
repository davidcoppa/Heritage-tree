import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { LocationEnum } from '../helpers/enums/location.enum';

export class ListObject {
  sort: MatSort;
  paginator: MatPaginator;
  abmObject: boolean = false;
  rowSelected: Object;
  type: LocationEnum //todo: Do it generic
}
