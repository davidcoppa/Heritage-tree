import { Person } from "./person.model";

export interface PersonApi {
    person: Person[];
    totalItems: number;

}