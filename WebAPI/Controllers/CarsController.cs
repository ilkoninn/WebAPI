
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.CarDTOs;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _rep;

        public CarsController(ICarRepository rep)
        {
            _rep = rep;
        }

        // <-- Get API Section -->
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IQueryable<Car> Cars = await _rep.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, Cars);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id < 0 && Id == null) return BadRequest();
            Car oldCar = await _rep.GetByIdAsync(Id);
            if (oldCar == null) return NotFound();


            return StatusCode(StatusCodes.Status200OK, oldCar);
        }


        // <-- Create API Section -->
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCarDTO createCarDTO)
        {

            Car newCar = new()
            {
                ModelYear = createCarDTO.ModelYear,
                DailyPrice = createCarDTO.DailyPrice,
                Description = createCarDTO.Description,
                ColorId = createCarDTO.ColorId,
                BrandId = createCarDTO.BrandId,
            };

            await _rep.CreateAsync(newCar);
            await _rep.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, newCar);
        }

        //<-- Update API Section -->
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCarDTO updateCarDTO)
        {
            if (updateCarDTO.Id < 0 && updateCarDTO.Id == null) return BadRequest();
            Car oldCar = await _rep.GetByIdAsync(updateCarDTO.Id);
            if (oldCar == null) return NotFound();

            oldCar.ModelYear = updateCarDTO.ModelYear;
            oldCar.Description = updateCarDTO.Description;
            oldCar.ColorId = updateCarDTO.ColorId;
            oldCar.BrandId = updateCarDTO.BrandId;
            oldCar.DailyPrice = updateCarDTO.DailyPrice;
            oldCar.CreatedDate = oldCar.CreatedDate;
            oldCar.UpdatedDate = DateTime.Now;

            _rep.UpdateAsync(oldCar);
            await _rep.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, oldCar);
        }

        //<-- Delete API Section -->
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id < 0 && Id == null) return BadRequest();
            Car oldCar = await _rep.GetByIdAsync(Id);
            if (oldCar == null) return NotFound();

            _rep.DeleteAsync(oldCar);
            await _rep.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, oldCar);
        }
    }
}
