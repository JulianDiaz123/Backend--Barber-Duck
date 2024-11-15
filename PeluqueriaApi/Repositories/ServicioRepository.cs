using Microsoft.EntityFrameworkCore;
using PeluqueriaApi.Config;
using System.Linq.Expressions;
using System.Linq;
using PeluqueriaApi.Models.Servicio;

namespace PeluqueriaApi.Repositories
{
    public interface IServicioRepository : IRepository<Servicio> { }
    public class ServicioRepository : Repository<Servicio>, IServicioRepository
    {
        public ServicioRepository(AppDbContext db) : base(db) { }

        public new async Task<Servicio> GetOne(Expression<Func<Servicio, bool>>? filter = null)
        {
            IQueryable<Servicio> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
