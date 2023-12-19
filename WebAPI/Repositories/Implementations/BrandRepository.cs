using System.Linq.Expressions;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories.Implementations
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<Brand>> GetAllAsync(Expression<Func<Brand, bool>>? expression = null, params string[] includes)
        {
            IQueryable<Brand> query = _context.Brands;
            if(expression is not null)
            {
                query = query.Where(expression);
            }
            if(includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query.Include(includes[i]);
                }
            }

            return query;
        }
        public async Task<Brand> GetByIdAsync(int Id)
        {
            return _context.Brands.AsNoTracking().FirstOrDefault(x => x.Id == Id);
        }
        public async Task CreateAsync(Brand newBrand)
        {
            await _context.AddAsync(newBrand);
        }
        public void UpdateAsync(Brand oldBrand)
        {
            _context.Update(oldBrand);
        }
        public void DeleteAsync(Brand oldBrand)
        {
            _context.Remove(oldBrand);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
