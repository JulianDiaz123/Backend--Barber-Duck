using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Rol.Dto
{
    public class UpdateUserRolesDTO
    {
        [Required]
        public List<int> RoleIds { get; set; } = null!;
    }
}
