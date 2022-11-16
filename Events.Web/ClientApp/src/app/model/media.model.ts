import { MediaType } from "./MediaType";

export interface Media {
    id: number;
    mediaDate: Date;
    mediaDateUploaded:Date;
    description:string;
    name:string;
    urlFile:string;
    mediaType:MediaType;

  }
