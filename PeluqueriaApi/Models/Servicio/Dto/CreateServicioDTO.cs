using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Servicio.Dto
{
    public class CreateServicioDTO
    {
        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; } = null!;

        [Required]
        public decimal Precio { get; set; }

       
    }
}
