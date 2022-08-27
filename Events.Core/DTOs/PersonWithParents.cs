using EventsManager.Model;

namespace Events.Core.DTOs
{
    public class PersonWithParents : Person
    {
        public Person? Father { get; set; }
        public Person? Mother { get; set; }
        public int? EventId { get; set; }
    }
}
