using Events.Core.Model;

namespace Events.Core.DTOs
{
    public class CountryReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string? Code { get; set; }
        public string? Region { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public List<States>? State { get; set; }

        public string FullName { get; set; }
    }
}
