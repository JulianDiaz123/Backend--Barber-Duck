namespace PeluqueriaApi.Models.Producto.Dto
{
    public class ProductosDTO
    {
        public int id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Marca { get; set; } = null!;
        public string Precio { get; set; } = null!;

        public string Url_imagen { get; set; }
    }
}
