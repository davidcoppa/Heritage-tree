using EventsManager.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Model
{
    public class Person
    {
        [Required]
        public int Id { get; set; }
        [Required]
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

    }
}
