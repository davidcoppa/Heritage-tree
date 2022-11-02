using EventsManager.Enums;
using EventsManager.Model;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.DTOs
{
    public class PersonWithParentsDTO
    {

        public int Id { get; set; }
        public List<PersonWithParentsDTO>? PersonSon { get; set; }
        //    public PersonWithParentsDTO? PersonMother { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? FirstSurname { get; set; }
        public string? SecondSurname { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? PlaceOfDeath { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfDeath { get; set; }
        public Gender Sex { get; set; }
        public List<Media>? Photos { get; set; }

        public int? Order { get; set; }


        public string Name { get; set; }
        public int Value { get; set; }
        public List<PersonWithParentsDTO>? children { get; set; }


    }
}
