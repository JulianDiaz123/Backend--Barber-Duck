using PeluqueriaApi.Models.Rol;
using PeluqueriaApi.Repositories;
using PeluqueriaApi.Utils.Exceptions;
using System.Net;

namespace PeluqueriaApi.Services
{
    public class RolServices
    {
        private readonly IRolRepository _rolRepository;
        
        public RolServices(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<Rol> GetOneByName(string name)
        {
            var rol = await _rolRepository.GetOne(r => r.Name == name);
            if(rol == null)
            {
                throw new CustomHttpException($"No se encontro el rol con el nombre{name}", HttpStatusCode.NotFound);
            }
            return rol;
        }

        public async Task<List<Rol>> GetManyByIds(List<int> rolIds)
        {
            if(rolIds == null || rolIds.Count == 0)
            {
                throw new CustomHttpException("No hay roles.", HttpStatusCode.BadRequest);
            }

            var roles = await _rolRepository.GetAll(r => rolIds.Contains(r.Id));
            return roles.ToList();
        }
    }
}
