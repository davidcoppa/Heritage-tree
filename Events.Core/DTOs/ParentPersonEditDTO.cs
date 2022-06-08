using EventsManager.Model;

namespace Events.Core.DTOs
{
    public class ParentPersonEditDTO
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public Person? PersonFather { get; set; }
        public Person? PersonMother { get; set; }
        public string? Description { get; set; }
    }
}
