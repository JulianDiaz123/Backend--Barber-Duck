using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Turno.Dto
{
    public class UpdateTurnoDTO
    {
        public DateTime? FH_turno { get; set; }

        public string? Metodo_pago { get; set; }

        public int? ServicioId { get; set; }
    }
}
