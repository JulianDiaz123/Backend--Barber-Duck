using AutoMapper;
using PeluqueriaApi.Config;
using PeluqueriaApi.Models.Servicio;
using PeluqueriaApi.Models.Servicio.Dto;
using PeluqueriaApi.Repositories;

namespace PeluqueriaApi.Services
{
    public class ServicioServices
    {
        private readonly IMapper _mapper;
        private readonly IServicioRepository _servicioRepository;

        public IMapper Mapper => _mapper;

        public ServicioServices(IMapper mapper, IServicioRepository servicioRepository)
        {
            _mapper = mapper;
            _servicioRepository = servicioRepository;
        }

        private async Task<Servicio> GetOneByIdOrException(int id)
        {
            var servicio = await _servicioRepository.GetOne(s => s.id == id);
            if (servicio == null)
            {
                throw new Exception($"No se encontro el servicio con Id = {id}");
            }
            return servicio;
        }

        public async Task<List<ServiciosDTO>> GetAll()
        {
            var servicios = await _servicioRepository.GetAll();
            return Mapper.Map<List<ServiciosDTO>>(servicios);
        }

        public async Task<ServicioDTO> GetOneById(int id)
        {
            var servicio = await GetOneByIdOrException(id);
            return Mapper.Map<ServicioDTO>(servicio);
        }

        public async Task<Servicio> CreateOne(CreateServicioDTO createServicioDto)
        {
            Servicio servicio = Mapper.Map<Servicio>(createServicioDto);
            await _servicioRepository.Add(servicio);
            return servicio;
        }

        public async Task<Servicio> UpdateOneById(int id, UpdateServicioDTO updateServicioDto)
        {
            Servicio servicio = await GetOneByIdOrException(id);
            var servicioMapped = Mapper.Map(updateServicioDto, servicio);
            await _servicioRepository.Update(servicioMapped);
            return servicioMapped;
        }

        public async Task DeleteOneById(int id)
        {
            var servicio = await GetOneByIdOrException(id);
            await _servicioRepository.Delete(servicio);
        }
    }
}
