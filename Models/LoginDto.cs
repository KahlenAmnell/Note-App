using System.ComponentModel.DataAnnotations;

namespace Note_App_API.Models
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
