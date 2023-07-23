using Events.Core.Model;

namespace Events.Core.DTOs
{
    public class CityCreateDTO
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string? Code { get; set; }
        public string? Region { get; set; }
        public string? Lat { get; set; }
        public string? Lng { get; set; }

        public States State { get; set; }

    }
}
