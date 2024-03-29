﻿using Events.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Model
{
    public class Event
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }
        public EventTypes EventType { get; set; }
        public Person Person1 { get; set; }
        public Person? Person2 { get; set; }
        public Person? Person3 { get; set; }
        public List<Media>? Media { get; set; }
        public Country? Loccation { get; set; }
        
    }
}
