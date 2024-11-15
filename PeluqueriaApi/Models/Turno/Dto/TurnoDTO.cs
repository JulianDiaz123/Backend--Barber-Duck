using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Turno.Dto
{
    public class TurnoDTO
    {
        public int id { get; set; }

        public DateTime FH_turno { get; set; }

        public string Metodo_pago { get; set; }

        public User.User User { get; set; } = null!;

        public Servicio.Servicio Servicio { get; set; } = null!;
    }
}
