using System.Linq.Expressions;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories.Implementations
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<Car>> GetAllAsync(Expression<Func<Car, bool>>? expression = null, params string[] includes)
        {
            IQueryable<Car> query = _context.Cars;
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
        public async Task<Car> GetByIdAsync(int Id)
        {
            return _context.Cars.AsNoTracking().FirstOrDefault(x => x.Id == Id);
        }
        public async Task CreateAsync(Car newCar)
        {
            await _context.AddAsync(newCar);
        }
        public void UpdateAsync(Car oldCar)
        {
            _context.Update(oldCar);
        }
        public void DeleteAsync(Car oldCar)
        {
            _context.Remove(oldCar);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
