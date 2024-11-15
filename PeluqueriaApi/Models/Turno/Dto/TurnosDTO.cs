namespace PeluqueriaApi.Models.Turno.Dto
{
    public class TurnosDTO
    {
        public int id { get; set; }

        public DateTime FH_turno { get; set; }


        public User.User User { get; set; } = null!;

        public Servicio.Servicio Servicio { get; set; } = null!;
    }
}
