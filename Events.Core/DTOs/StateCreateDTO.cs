using Events.Core.Model;

namespace Events.Core.DTOs
{
    public class StateCreateDTO
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string? Code { get; set; }
        public string? Region { get; set; }
        public string? FullName { get; set; }
        public string? Coordinates { get; set; }
        public int CountryId { get; set; }
        public List<City>? Cities { get; set; }
    }
}
