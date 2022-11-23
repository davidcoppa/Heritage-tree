
export interface FileData {
  id: number | undefined,
  name: string,
  dateUploaded: Date | undefined,
  description: string,
  mediaType: string;
  size: number,
  url: string,
  webUrl: string,

  fileUploaded: boolean
}
