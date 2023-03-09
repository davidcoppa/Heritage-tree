import { MediaType } from "./MediaType";

export interface FileData {
  id: number | undefined,
  name: string,
  dateUploaded: Date | undefined,
  description: string,
  //documentType: string;
  documentType: MediaType;
  size: number,
  url: string,
  webUrl: string,

  fileUploaded: boolean
}
