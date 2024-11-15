using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeluqueriaApi.Models.Rol
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; } = null!;
    }

    public class RolUsers
    {
        public int RolId { get; set; }
        public int UserId { get; set; }
    }
}
