using Events.Core.Model;
using EventsManager.Model;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.DTOs
{
    public class EventCreateDTO
    {
            public string Title { get; set; }

            public string? Description { get; set; }

            [DataType(DataType.Date)]
            public DateTime? EventDate { get; set; }
            public EventTypes EventType { get; set; }
            public Person Person1 { get; set; }
            public Person? Person2 { get; set; }
            public Person? Person3 { get; set; }
            public List<Photos>? photos { get; set; }
            public Location? Loccation { get; set; }

    }
}
