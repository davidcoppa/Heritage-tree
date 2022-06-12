import { Events } from "./event.model";

export interface EventApi{
    event: Events[];
    totalItems: number;
}