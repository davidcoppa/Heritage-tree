using Events.Core.Model;

namespace Events.Core.DTOs
{
    public class MediaDTO
    {
        public int Id { get; set; }
        public DateTime? DateUploaded { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public List<FileDataDTO>? File { get; set; }
        public EventCreateEditDTO? Event { get; set; }
        public List<TagsEditDTO>? TagItems { get; set; }

        public List<FileDataDTO>? OnlyFilesInfo { get; set; }


    }
}
