using System.Linq.Expressions;

namespace WebAPI.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task<IQueryable<Brand>> GetAllAsync(Expression<Func<Brand, bool>>? expression = null, params string[] includes);
        Task<Brand> GetByIdAsync(int Id);
        Task CreateAsync(Brand newBrand);
        void UpdateAsync(Brand oldBrand);
        void DeleteAsync(Brand oldBrand);
        Task SaveChangesAsync();
    }
}
