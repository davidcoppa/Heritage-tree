using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Model
{
    public class Photos
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public string? UrlFile { get; set; }


    }
}
