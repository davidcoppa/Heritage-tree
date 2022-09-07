import { MatTableDataSource } from "@angular/material/table";
import { State } from "./state.model";

export interface Country {
  id: number,
  name: string,
  capital: string,
  code: string,
  region: string,
  lat: number,
  lgn: number,
  state: State[] | MatTableDataSource<State>,
  fullName: string
}
