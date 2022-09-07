import { MatTableDataSource } from "@angular/material/table";
import { City } from "./city.model";

export interface State {
  id: number,
  name: string,
  capital: string,
  code: string,
  region: string,

  lat: number,
  lgn: number,

  cities: City[] | MatTableDataSource<City> ,
  fullName: string
}
