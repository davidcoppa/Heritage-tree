import { Photos } from "./photos.model";

export interface Person {
  Id: number;
  FirstName: string;
  SecondName: string;
  FirstSurname: string;
  SecondSurname: string;
  PlaceOfBirth: string;
  PlaceOfDeath: string;
  Sex: number;
  DateBirth: Date;
  DateDeath: Date;
  Order: number;
  Photos: Photos[];

}