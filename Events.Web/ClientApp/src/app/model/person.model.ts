import { Photos } from "./photos.model";

export interface Person {
  id: number;
  firstName: string;
  secondName: string;
  firstSurname: string;
  secondSurname: string;
  placeOfBirth: string;
  placeOfDeath: string;
  sex: number;
  dateBirth: Date;
  dateDeath: Date;
  order: number;
  photos: Photos[];

}