
using System.Text.RegularExpressions;
using WebAPI.DTOs.CarDTOs;

namespace WebAPI.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper, AppDbContext context = null)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<IQueryable<Brand>> GetAllAsync(Expression<Func<Brand, bool>>? expression = null, params string[] includes)
        {
            return await _brandRepository.GetAllAsync(expression, includes);
        }
        public async Task<Brand> GetByIdAsync(int Id)
        {
            return await _brandRepository.GetByIdAsync(Id);
        }
        public async Task<Brand> CreateAsync(CreateBrandDTO createBrandDTO)
        {
            Brand newBrand = _mapper.Map<Brand>(createBrandDTO);

            _brandRepository.CreateAsync(newBrand);
            _brandRepository.SaveChangesAsync();

            return newBrand;
        }
        public async Task<Brand> UpdateAsync(UpdateBrandDTO updateBrandDTO)
        {
            Brand oldBrand = await _brandRepository.GetByIdAsync(updateBrandDTO.Id);
            _mapper.Map(updateBrandDTO, oldBrand);

            _brandRepository.UpdateAsync(oldBrand);
            _brandRepository.SaveChangesAsync();

            return oldBrand;
        }
        public async void DeleteAsync(int Id)
        {
            Brand oldBrand = await _brandRepository.GetByIdAsync(Id);

            _brandRepository.DeleteAsync(oldBrand); 
            _brandRepository.SaveChangesAsync();   
        }

    }
}
