using System.Linq.Expressions;
using System.Linq;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }
        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query.Include(includes[i]);
                }
            }

            return query;
        }
        public async Task<T> GetByIdAsync(int Id)
        {
            return _table.AsNoTracking().FirstOrDefault(x => x.Id == Id);
        }
        public async Task CreateAsync(T entity)
        {
            await _table.AddAsync(entity);
        }
        public void UpdateAsync(T entity)
        {
            _table.Update(entity);
        }
        public void DeleteAsync(T entity)
        {
            _table.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
