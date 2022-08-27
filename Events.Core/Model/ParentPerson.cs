using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Model
{
    public class ParentPerson
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public Person Person { get; set; }
        public Person? PersonFather { get; set; }
        public Person? PersonMother { get; set; }
        public string? Description { get; set; }    
    }
}
