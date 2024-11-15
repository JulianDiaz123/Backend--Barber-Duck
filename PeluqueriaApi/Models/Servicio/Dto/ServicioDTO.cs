using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Servicio.Dto
{
    public class ServicioDTO
    {
        public int id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }


    }
}
