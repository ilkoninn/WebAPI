﻿using System.Net;

namespace WebAPI.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _CarRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository CarRepository, IMapper mapper, AppDbContext context = null)
        {
            _CarRepository = CarRepository;
            _mapper = mapper;
        }

        public async Task<IQueryable<Car>> GetAllAsync(Expression<Func<Car, bool>>? expression = null, params string[] includes)
        {
            return await _CarRepository.GetAllAsync(expressionOrder: x => x.ModelYear, isDescending:true);      
        }
        public async Task<Car> GetByIdAsync(int Id)
        {
            var oldCar = await _CarRepository.GetByIdAsync(Id);
            if (oldCar is not null)
            {
                return oldCar;
            }
            else
            {
                throw new Exception("Not Found");
            }
        }
        public async Task<Car> CreateAsync(CreateCarDTO createCarDTO)
        {
            Car newCar = _mapper.Map<Car>(createCarDTO);

            _CarRepository.CreateAsync(newCar);
            _CarRepository.SaveChangesAsync();

            return newCar;
        }
        public async Task<Car> UpdateAsync(UpdateCarDTO updateCarDTO)
        {
            Car oldCar = await _CarRepository.GetByIdAsync(updateCarDTO.Id);
            _mapper.Map(updateCarDTO, oldCar);

            _CarRepository.UpdateAsync(oldCar);
            _CarRepository.SaveChangesAsync();

            return oldCar;
        }

        public async void DeleteAsync(int Id)
        {
            Car oldCar = await _CarRepository.GetByIdAsync(Id);

            _CarRepository.DeleteAsync(oldCar);
            _CarRepository.SaveChangesAsync();
        }
    }
}
