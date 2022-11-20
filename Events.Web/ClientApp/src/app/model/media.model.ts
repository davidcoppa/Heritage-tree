import { MediaType } from "./MediaType";

export interface Media {
  id: number;
  name: string;
  dateUploaded: Date;
  description: string;
  event: Event;
}
