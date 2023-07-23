using System.ComponentModel.DataAnnotations;

namespace Events.Core.DTOs
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
       
        [Required]
        public string Password { get; set; }
        public string? Password2 { get; set; }
        [Required]
        public string CaptchaToken { get; set; }
    }
}
