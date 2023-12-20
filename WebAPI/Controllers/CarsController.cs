
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.CarDTOs;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _service;

        public CarsController(ICarService service)
        {
            _service = service;
        }

        // <-- Get API Section -->
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IQueryable<Car> Cars = await _service.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, Cars);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            Car oldCar = await _service.GetByIdAsync(Id);

            return StatusCode(StatusCodes.Status200OK, oldCar);
        }


        // <-- Create API Section -->
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCarDTO createCarDTO)
        {

            await _service.CreateAsync(createCarDTO);

            return StatusCode(StatusCodes.Status201Created);
        }

        //<-- Update API Section -->
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCarDTO updateCarDTO)
        {
            var result = _service.UpdateAsync(updateCarDTO);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        //<-- Delete API Section -->
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            _service.DeleteAsync(Id);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
