namespace Events.Core.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? lat { get; set; }
        public string? lgn { get; set; }
        public string? stringName { get; set; }

        public string GetStringName()
        {
            return Country + ", " + City + ", " + Town;
        }
    }
}
