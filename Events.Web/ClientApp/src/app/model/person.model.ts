import { Media } from "./media.model";

export interface Person {
  id: number;
  firstName: string;
  secondName: string;
  firstSurname: string;
  secondSurname: string;
  placeOfBirth: string;
  placeOfDeath: string;
  sex: number;
  dateOfBirth: Date | undefined;
  dateOfDeath: Date | undefined;
  order: number;
  media: Media[];

  father: Person;
  mother: Person;
 // eventTypeId: number;
  eventId: number;

}
