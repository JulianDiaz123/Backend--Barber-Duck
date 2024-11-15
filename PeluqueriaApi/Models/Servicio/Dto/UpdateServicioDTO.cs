using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Servicio.Dto
{
    public class UpdateServicioDTO
    {
        [MaxLength(40)]
        public string? Nombre { get; set; }

        [MaxLength(250)]
        public string? Descripcion { get; set; }

        public decimal? Precio { get; set; }


    }
}
