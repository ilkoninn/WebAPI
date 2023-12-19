using System.Linq.Expressions;

namespace WebAPI.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<IQueryable<Car>> GetAllAsync(Expression<Func<Car, bool>>? expression = null, params string[] includes);
        Task<Car> GetByIdAsync(int Id);
        Task CreateAsync(Car newCar);
        void UpdateAsync(Car oldCar);
        void DeleteAsync(Car oldCar);
        Task SaveChangesAsync();
    }
}
