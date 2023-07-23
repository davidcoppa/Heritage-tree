import { MatTableDataSource } from "@angular/material/table";
import { City } from "./city.model";

export interface State {
  id: number,
  name: string,
  capital: string,
  code: string,
  region: string,

  lat: string,
  lng: string,

  cities: City[] | MatTableDataSource<City> ,
  fullName: string
  countryId: number
}
