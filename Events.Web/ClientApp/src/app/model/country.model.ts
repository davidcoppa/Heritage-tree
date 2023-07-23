import { MatTableDataSource } from "@angular/material/table";
import { State } from "./state.model";

export interface Country {
  id: number,
  name: string,
  capital: string,
  code: string,
  region: string,
  lat: string,
  lng: string,
  state: State[] | MatTableDataSource<State>,
  fullName: string
}
