using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Auth
{
    public class Login
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } = null!;
    }
}
