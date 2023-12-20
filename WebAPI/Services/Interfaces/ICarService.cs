using System.Linq.Expressions;
using WebAPI.DTOs.CarDTOs;

namespace WebAPI.Services.Interfaces
{
    public interface ICarService 
    {
        Task<IQueryable<Car>> GetAllAsync(Expression<Func<Car, bool>>? expression = null, params string[] includes);
        Task<Car> GetByIdAsync(int Id);
        Task<Car> CreateAsync(CreateCarDTO entity);
        Task<Car> UpdateAsync(UpdateCarDTO entity);
        void DeleteAsync(int Id);
    }
}
