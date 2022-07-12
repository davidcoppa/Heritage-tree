import { Media } from "./media.model";

export interface Person {
  Id: number;
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
  media: Media[];

}