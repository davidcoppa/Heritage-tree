using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Model
{
    public class ParentPerson
    {
        public Person Person { get; set; }
        public Person? PersonFather { get; set; }
        public Person? PersonMother { get; set; }
    }
}
