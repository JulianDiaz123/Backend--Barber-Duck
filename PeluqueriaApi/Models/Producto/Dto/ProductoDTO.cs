using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeluqueriaApi.Models.Producto.Dto
{
    public class ProductoDTO
    {
        public int id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Marca { get; set; } = null!;

        public string Descripcion { get; set; }

        public string Categoria { get; set; } = null!;

        public decimal Precio { get; set; }

        public int Existencias { get; set; }

        public string Url_imagen { get; set; }
    }
}
