using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Turno.Dto
{
    public class CreateTurnoDTO
    {
        [Required]
        public DateTime FH_turno { get; set; }

        [Required]
        public string Metodo_pago { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ServicioId { get; set; }
    }
}
