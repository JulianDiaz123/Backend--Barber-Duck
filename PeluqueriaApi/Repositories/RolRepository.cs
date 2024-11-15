using PeluqueriaApi.Config;
using PeluqueriaApi.Models.Rol;

namespace PeluqueriaApi.Repositories
{
    public interface IRolRepository : IRepository<Rol> { }
    public class RolRepository : Repository<Rol>, IRolRepository
    {
        public RolRepository(AppDbContext db) : base(db)
        {
        }
    }
}
