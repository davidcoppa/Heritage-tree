using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Model
{
    public class ParentPerson
    {
        public int ID { get; set; } 
        public Person Person { get; set; }
        public Person? PersonFather { get; set; }
        public Person? PersonMother { get; set; }
        public string? Description { get; set; }    
    }
}
