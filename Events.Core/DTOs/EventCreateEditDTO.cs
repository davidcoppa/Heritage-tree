using Events.Core.Model;
using EventsManager.Model;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.DTOs
{
    public class EventCreateEditDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }
        public EventTypes? EventType { get; set; }
        //public PersonWithParents Person1 { get; set; }
        //public PersonWithParents? Person2 { get; set; }
        //public PersonWithParents? Person3 { get; set; }
        public Person? Person1 { get; set; }
        public Person? Person2 { get; set; }
        public Person? Person3 { get; set; }
        public List<MediaDTO>? Media { get; set; }
        public Country? Loccation { get; set; }
    }
}
