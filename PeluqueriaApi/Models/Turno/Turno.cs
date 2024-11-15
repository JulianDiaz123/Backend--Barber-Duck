using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Turno
{
    public class Turno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public DateTime FH_turno { get; set; } = DateTime.Now;


        [Required]
        public string Metodo_pago { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ServicioId { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; } = null!;

        [ForeignKey("ServicioId")]
        public Servicio.Servicio Servicio { get; set; } = null!;
    }
}
