import { Events } from "./event.model";
import { FileData } from "./fileData.model";

export class Media {
  id: number ;
  name: string;
  dateUploaded: Date;
  description: string;
  file: FileData[];
  event: Events|null;
}
