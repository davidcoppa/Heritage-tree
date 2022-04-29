using Events.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Model
{
    public class Event
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }
        public EventTypes EventType { get; set; }
        public ParentPerson Person1 { get; set; }
        public ParentPerson? Person2 { get; set; }
        public ParentPerson? Person3 { get; set; }
        public List<Photos>? photos { get; set; }
        public Location? Loccation { get; set; }
        
    }
}
