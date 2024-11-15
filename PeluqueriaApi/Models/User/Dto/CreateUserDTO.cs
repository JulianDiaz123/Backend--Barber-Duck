using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.User.Dto
{
    public class CreateUserDTO
    {
        [Required]
        [MaxLength(40)]
        public string apelNom { get; set; } = null!;

        [EmailAddress]
        public string email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string password { get; set; } = null!;

        [Required]
        public string telefono { get; set; } = null!;

        [Required]
        [MaxLength(40)]
        public string userName { get; set; } = null!;
    }
}
