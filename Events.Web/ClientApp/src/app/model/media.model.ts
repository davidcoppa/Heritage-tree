import { MediaType } from "./MediaType";
import { Tags } from "./Tags";

export interface Media {
  Id: number;
  MediaDate: Date;
  MediaDateUploaded: Date;
  Description: string;
  Name: string;
  UrlFile: string;
  MediaType: MediaType;
  Tags: Tags[];

}
