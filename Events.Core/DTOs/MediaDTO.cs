using Events.Core.Model;
using EventsManager.Model;

namespace Events.Core.DTOs
{
    public class MediaDTO
    {
        public int Id { get; set; }
        public DateTime? DateUploaded { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public List<FileData>? File { get; set; }
        public Event? Event { get; set; }
        public List<Tags>? TagItems { get; set; }

        public List<FileData>? OnlyFilesInfo { get; set; }


    }
}
