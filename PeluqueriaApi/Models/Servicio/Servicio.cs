using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Servicio
{
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }
        
        [Required]
        public decimal Precio { get; set; }

       
    }
}
