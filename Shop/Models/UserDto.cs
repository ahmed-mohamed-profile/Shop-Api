using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
