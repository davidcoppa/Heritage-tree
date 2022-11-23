using Events.Core.Model;
using EventsManager.Model;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.DTOs
{
    public class MediaCreateDTO
    {
        
        [DataType(DataType.Date)]
        public DateTime? MediaDateUploaded { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public List<FileData>? File{ get; set; }
        public Event? Event { get; set; }
    }
}
