using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.User.Dto
{
    public class UserDTO
    {
        public int id { get; set; }

        public string apelNom { get; set; } = null!;

        public string email { get; set; } = null!;

        public string telefono { get; set; } = null!;

        public string userName { get; set; } = null!;
    }
}
