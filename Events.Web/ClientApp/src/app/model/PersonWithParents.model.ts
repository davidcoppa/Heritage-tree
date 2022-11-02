import { Media } from "./media.model";
import { Person } from "./person.model";

export interface PersonWithParents {
  id: number;
  firstName: string;
  firstSurname: string;
  secondName: string;

  secondSurname: string;
  placeOfBirth: string;
  placeOfDeath: string;
  sex: number;
  dateOfBirth: Date;
  dateOfDeath: Date;
  order: number;

  media: Media[];

 // personSon: PersonWithParents[];
  father: Person;
  mother: Person;
  eventId: number;
}

export interface rootValues {
  name: string,
  value: number,
  children: rootValues[],


  id: number;
  firstName: string;
  firstSurname: string;
  secondName: string;

  secondSurname: string;
  placeOfBirth: string;
  placeOfDeath: string;
  sex: number;
  dateOfBirth: Date;
  dateOfDeath: Date;
  order: number;

  media: Media[];

  // personSon: PersonWithParents[];
  father: Person;
  mother: Person;
  eventId: number;
}
