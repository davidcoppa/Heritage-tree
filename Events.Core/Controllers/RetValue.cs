using Events.Core.DTOs;

namespace Events.Core.Controllers
{
    internal class RetValue
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public List<RetValue>? children { get; set; }
    }
}