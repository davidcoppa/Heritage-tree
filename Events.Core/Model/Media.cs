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
        public DateTime? MediaDateUploaded { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public List<FileData>? File { get; set; }
        public Event? Event { get; set; }

    }
}
