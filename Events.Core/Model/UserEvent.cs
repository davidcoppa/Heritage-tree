using EventsManager.Model;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Core.Model
{
    public class UserEvent
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public string FamilyName { get; set; }
        public bool IsMainUser{ get; set; }
        public IdentityUser User { get; set; }
        public Event Event { get; set; }
    }
}
