import { MediaType } from "./MediaType";

export interface Media {
  id: number;
  mediaName: string;
  mediaDate: Date;
  mediaDateUploaded: Date;
  description: string;
  mediaType: MediaType;
  title: string | undefined;
  urlFile: string;
  webUrl: string | undefined;
  size: Int32Array | undefined;
}
