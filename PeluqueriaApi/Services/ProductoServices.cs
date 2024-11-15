using AutoMapper;
using PeluqueriaApi.Config;
using PeluqueriaApi.Models.Producto;
using PeluqueriaApi.Models.Producto.Dto;
using PeluqueriaApi.Repositories;
using System.Net;

namespace PeluqueriaApi.Services
{
    public class ProductoServices
    {
        private readonly IMapper _mapper;
        private readonly IProductoRepository _productoRepository;

        public ProductoServices(IMapper mapper, IProductoRepository productoRepository)
        {
            _mapper = mapper;
            _productoRepository = productoRepository;
        }

        private async Task<Producto> GetOneByIdOrException(int id)
        {
            var producto = await _productoRepository.GetOne(p => p.id == id);
            if (producto == null)
            {
                throw new Exception($"No se encontro el producto con Id = {id}");
            }
            return producto;
        }

        public async Task<List<ProductosDTO>> GetAll()
        {
            var productos = await _productoRepository.GetAll();
            return _mapper.Map<List<ProductosDTO>>(productos);
        }

        public async Task<ProductoDTO> GetOneById(int id)
        {
            var producto = await GetOneByIdOrException(id);
            return _mapper.Map<ProductoDTO>(producto);
        }

        public async Task<Producto> CreateOne(CreateProductoDTO createProductoDto)
        {
            var producto = _mapper.Map<Producto>(createProductoDto);
            await _productoRepository.Add(producto);
            return producto;
        }

        public async Task<Producto> UpdateOneById(int id, UpdateProductoDTO updateProductoDTO)
        {
            var producto = await GetOneByIdOrException(id);
            var productoMapped = _mapper.Map(updateProductoDTO, producto);
            await _productoRepository.Update(productoMapped);
            return productoMapped;
        }

        public async Task DeleteOneById(int id)
        {
            var producto = await GetOneByIdOrException(id);
            await _productoRepository.Delete(producto);
        }
    }
}
