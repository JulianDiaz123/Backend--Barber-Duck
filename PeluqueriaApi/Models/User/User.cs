using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeluqueriaApi.Models.User
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

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

        public List<Rol.Rol> Roles { get; set; } = null!;
    }
}
