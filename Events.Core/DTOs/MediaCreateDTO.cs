using Events.Core.Model;
using EventsManager.Model;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.DTOs
{
    public class MediaCreateDTO
    {
        
        [DataType(DataType.Date)]
        public DateTime? DateUploaded { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public List<FileDataCreateDTO>? File{ get; set; }
        public EventCreateEditDTO? Event { get; set; }
        public List<TagsDTO>? TagItems { get; set; }
    }
}
