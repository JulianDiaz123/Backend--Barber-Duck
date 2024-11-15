using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Producto.Dto
{
    public class UpdateProductoDTO
    {
        [MaxLength(40)]
        public string? Nombre { get; set; } 

        [MaxLength(250)]
        public string? Descripcion { get; set; }

        [MaxLength(40)]
        public string? Categoria { get; set; } 

        [MaxLength(40)]
        public string? Marca { get; set; } 

        public decimal? Precio { get; set; }

        public int? Existencias { get; set; }

        [Url]
        public string? Url_imagen { get; set; }
    }
}
