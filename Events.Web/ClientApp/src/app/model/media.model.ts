import { MatTableDataSource } from "@angular/material/table";
import { Events } from "./event.model";
import { FileData } from "./fileData.model";
import { TagItem } from "./tagItem.model";

export interface Media {
  id: number ,
  name: string,
  dateUploaded: Date,
  description: string,
  file: FileData[] | MatTableDataSource<FileData>,
  event: Events | null,
  tagItems: TagItem[] 

}
