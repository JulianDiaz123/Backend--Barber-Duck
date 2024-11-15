using Microsoft.EntityFrameworkCore;
using PeluqueriaApi.Models.Producto;
using System.Linq.Expressions;
using System.Linq;
using PeluqueriaApi.Config;

namespace PeluqueriaApi.Repositories
{
    public interface IProductoRepository : IRepository<Producto> { }
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        public ProductoRepository(AppDbContext db) : base(db) { }

        public new async Task<Producto> GetOne(Expression<Func<Producto, bool>>? filter = null)
        {
            IQueryable<Producto> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
