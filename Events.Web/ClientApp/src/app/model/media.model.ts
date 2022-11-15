import { MediaType } from "./MediaType";

export interface Media {
    Id: number;
    MediaDate: Date;
    MediaDateUploaded:Date;
    Description:string;
    Name:string;
    UrlFile:string;
    MediaType:MediaType;

  }