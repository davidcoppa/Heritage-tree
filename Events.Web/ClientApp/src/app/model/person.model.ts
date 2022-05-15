import { Photos } from "./photos.model";

export interface Person {
    id: number;
    firsName: string;
    secondName: string;
    frstSurname: string;
    secondSurname: string;
    placeOfBirth: string;
    PlaceOfDeath: string;
    sex: number;
    dateBirth: Date;
      dateDeath: Date;
      order:number;
      photos:Photos[];
  }