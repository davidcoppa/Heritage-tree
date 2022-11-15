using Events.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Model
{
    public class Media
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? MediaDate { get; set; }
        public DateTime? MediaDateUploaded { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public string? UrlFile { get; set; }
        public MediaType MediaType { get; set; }
        public List<Tags>? MediaTag{ get; set; }

    }
}
