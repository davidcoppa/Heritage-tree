import { QueryList } from "@angular/core";
import { MatSort } from "@angular/material/sort";
import { MatTable } from "@angular/material/table";

export class ListSubObject {
  innerTable: QueryList<MatTable<Object>>;
  innerSortTable: QueryList<MatSort>;
  valueSelected: Object;
  element: Object;


}
