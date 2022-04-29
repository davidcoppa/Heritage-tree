﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Model
{
    public class Event
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }
        public  EventTypes EventType { get; set; }
        public Person Person1 { get; set; }
        public Person? Person2 { get; set; }
      
        public string Location { get; set; }

        public string lat { get; set; }
        public string lgn { get; set; }
    }
}
