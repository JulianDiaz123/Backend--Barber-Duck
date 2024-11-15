using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeluqueriaApi.Config;
using PeluqueriaApi.Models.Turno;
using PeluqueriaApi.Models.Turno.Dto;
using PeluqueriaApi.Repositories;
using PeluqueriaApi.Utils.Exceptions;
using System.Net;

namespace PeluqueriaApi.Services
{
    public class TurnoServices
    {
        private readonly IMapper _mapper;
        private readonly ITurnoRepository _turnoRepository;
        private readonly ServicioServices _servicioServices;
        private readonly UserServices _userServices;

        public TurnoServices(IMapper mapper, ITurnoRepository turnoRepository, ServicioServices servicioServices, UserServices userServices)
        {
            _mapper = mapper;
            _turnoRepository = turnoRepository;
            _servicioServices = servicioServices;
            _userServices = userServices;
        }

        private async Task<Turno> GetOneByIdOrException(int id)
        {
            // Icluimos la entidad Servicio y User para que la traiga con la consulta.
            var turno = await _turnoRepository.GetOne(t => t.id == id);
            if (turno == null)
            {
                throw new CustomHttpException($"No se encontro el turno con Id = {id}", HttpStatusCode.NotFound);
            }
            return turno;
        }

        public async Task<List<TurnosDTO>> GetAll()
        {
            var turno = await _turnoRepository.GetAll();
            return _mapper.Map<List<TurnosDTO>>(turno);
        }

        public async Task<TurnoDTO> GetOneById(int id)
        {
            var turno = await GetOneByIdOrException(id);
            //var servicio = _servicioServices.GetOneById(turno.ServicioId);
            //auto.Servicio = servicio;
            return _mapper.Map<TurnoDTO>(turno);
        }

        public async Task<Turno> CreateOne(CreateTurnoDTO createTurnoDto)
        {
            Turno turno = _mapper.Map<Turno>(createTurnoDto);
            //Verifica que existe el servicio
            await _servicioServices.GetOneById(turno.ServicioId);

            //Verifica que existe el usuario
            await _userServices.GetOneById(turno.UserId);

            await _turnoRepository.Add(turno);
            return turno;
        }

        public async Task<Turno> UpdateOneById(int id, UpdateTurnoDTO updateTurnoDto)
        {
            Turno turno = await GetOneByIdOrException(id);

            var turnoMapped = _mapper.Map(updateTurnoDto, turno);

            await _servicioServices.GetOneById(turnoMapped.ServicioId);

            await _userServices.GetOneById(turnoMapped.UserId);

            await _turnoRepository.Update(turnoMapped);

            return turnoMapped;
        }

        public async Task DeleteOneById(int id)
        {
            var turno = await GetOneByIdOrException(id);

            await _turnoRepository.Delete(turno);
        }
    }
}
