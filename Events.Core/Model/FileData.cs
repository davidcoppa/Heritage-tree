namespace Events.Core.Model
{
    public class FileData
    {
        public int Id { get; set; }
        public DateTime DateUploaded { get; set; }
        public string Name { get; set; }
        //public string DocumentType { get; set; }
        public MediaType DocumentType { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlPreview { get; set; }
        public string WebUrl { get; set; }
    }
}
