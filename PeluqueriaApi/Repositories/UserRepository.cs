using PeluqueriaApi.Models.User;
using PeluqueriaApi.Config;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PeluqueriaApi.Repositories
{ 
    public interface IUserRepository : IRepository<User> { }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db) { }

        public new async Task<User> GetOne(Expression<Func<User, bool>>? filter = null)
        {
            IQueryable<User> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter).Include(u => u.Roles);
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
