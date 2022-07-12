import { EventType } from "./eventType.model";
import { Media } from "./media.model";
import { Location } from "./location.model";
import { Person } from "./person.model";



export interface Events {
    Id:number,
    Title:string,
    Description:string,
    EventDate:Date,
    EventType:EventType,
    Person1:Person,
    Person2:Person,
    Person3:Person,
    Location:Location,
    Media:Media[]


}