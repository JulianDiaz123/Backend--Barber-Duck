using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.User.Dto
{
    public class UpdateUserDTO
    {
        [MaxLength(40)]
        public string? apelNom { get; set; } = null!;

        [EmailAddress]
        public string? email { get; set; } = null!;
       
        public string? telefono { get; set; } = null!;

        [MaxLength(40)]
        public string? userName { get; set; } = null!;
    }
}
