import { EventType } from "./eventType.model";
import { Media } from "./media.model";
import { Country } from "./country.model";
import { Person } from "./person.model";



export interface Events {
  id: number,
  title: string,
  description: string,
  eventDate: Date,
  eventType: EventType,
  person1: Person,
  person2: Person,
  person3: Person,
  location: Country,
  media: Media[]


}
