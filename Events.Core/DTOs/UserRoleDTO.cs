using System.ComponentModel.DataAnnotations;

namespace Events.Core.DTOs
{
    public class UserRoleDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Rol { get; set; }
    }
}
