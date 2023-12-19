using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories.Implementations;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _rep;

        public BrandsController(IBrandRepository rep)
        {
            _rep = rep;
        }

        // <-- Get API Section -->
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IQueryable<Brand> brands = await _rep.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, brands);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            if(Id < 0 && Id == null) return BadRequest();
            Brand oldBrand = await _rep.GetByIdAsync(Id);
            if(oldBrand == null) return NotFound();


            return StatusCode(StatusCodes.Status200OK, oldBrand);
        }


        // <-- Create API Section -->
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBrandDTO createBrandDTO)
        {
            Brand newBrand = new()
            {
                Name = createBrandDTO.Name,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            await _rep.CreateAsync(newBrand);
            await _rep.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, newBrand);
        }

        //<-- Update API Section -->
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateBrandDTO updateBrandDTO)
        {
            if (updateBrandDTO.Id < 0 && updateBrandDTO.Id == null) return BadRequest();
            Brand oldBrand = await _rep.GetByIdAsync(updateBrandDTO.Id);
            if (oldBrand == null) return NotFound();

            oldBrand.Name = updateBrandDTO.Name;
            oldBrand.CreatedDate = oldBrand.CreatedDate;
            oldBrand.UpdatedDate = DateTime.Now;

            _rep.UpdateAsync(oldBrand);
            await _rep.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, oldBrand);
        }

        //<-- Delete API Section -->
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id < 0 && Id == null) return BadRequest();
            Brand oldBrand = await _rep.GetByIdAsync(Id);
            if (oldBrand == null) return NotFound();

            _rep.DeleteAsync(oldBrand);
            await _rep.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, oldBrand);
        }


    }
}
