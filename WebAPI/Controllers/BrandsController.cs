using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories.Implementations;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandsController(IBrandService service)
        {
            _service = service;
        }

        // <-- Get API Section -->
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IQueryable<Brand> brands = await _service.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, brands);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            Brand oldBrand = await _service.GetByIdAsync(Id);

            return StatusCode(StatusCodes.Status200OK, oldBrand);
        }


        // <-- Create API Section -->
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBrandDTO createBrandDTO)
        {

            await _service.CreateAsync(createBrandDTO);

            return StatusCode(StatusCodes.Status201Created);
        }

        //<-- Update API Section -->
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateBrandDTO updateBrandDTO)
        {
            var result =  _service.UpdateAsync(updateBrandDTO);

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
