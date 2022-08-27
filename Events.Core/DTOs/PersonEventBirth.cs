using EventsManager.Model;

namespace Events.Core.DTOs
{
    public class PersonEventBirth
    {
        public Person PersonSon { get; set; }
        public Person? PersonFather { get; set; }
        public Person? PersonMother { get; set; }
        public int? EventId { get; set; }
    }
}
