using EventsManager.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Core.Model
{
    public class MediaTags
    {
        public int Id { get; set; }
        public Media? Media { get; set; }
        public Tags? Tags { get; set; }
    }
}
