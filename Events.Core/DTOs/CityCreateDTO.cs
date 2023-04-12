using Events.Core.Model;

namespace Events.Core.DTOs
{
    public class CityCreateDTO
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string? Code { get; set; }
        public string? Region { get; set; }
        public string? Coordinates { get; set; }
        public States State { get; set; }

    }
}
