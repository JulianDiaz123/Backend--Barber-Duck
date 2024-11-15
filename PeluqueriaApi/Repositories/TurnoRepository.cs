using Microsoft.EntityFrameworkCore;
using PeluqueriaApi.Config;
using PeluqueriaApi.Models.User;
using System.Linq.Expressions;
using System.Linq;
using PeluqueriaApi.Models.Turno;

namespace PeluqueriaApi.Repositories
{
    public interface ITurnoRepository : IRepository<Turno> { }

    public class TurnoRepository : Repository<Turno>, ITurnoRepository
    {
        public TurnoRepository(AppDbContext db) : base(db) { }

        public new async Task<Turno> GetOne(Expression<Func<Turno, bool>>? filter = null)
        {
            IQueryable<Turno> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter).Include(t => t.User).Include(t => t.Servicio);
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
