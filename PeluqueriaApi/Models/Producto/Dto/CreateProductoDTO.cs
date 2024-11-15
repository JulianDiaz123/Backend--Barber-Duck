using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Producto.Dto
{
    public class CreateProductoDTO
    {
        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; } = null!;

        [Required]
        [MaxLength(40)]
        public string Categoria { get; set; } = null!;

        [Required]
        [MaxLength(40)]
        public string Marca { get; set; } = null!;

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Existencias { get; set; }

        [Required]
        [Url]
        public string Url_imagen { get; set; } = null!;
    }
}
