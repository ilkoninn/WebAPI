using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BrandsController(AppDbContext db)
        {
            _db = db;
        }

        // <-- Get API Section -->
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Brand> brands = await _db.Brands.ToListAsync();

            return StatusCode(StatusCodes.Status200OK, brands);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            if(Id < 0 && Id == null) return BadRequest();
            Brand oldBrand = await _db.Brands.FirstOrDefaultAsync(x => x.Id == Id);
            if(oldBrand == null) return NotFound();


            return StatusCode(StatusCodes.Status200OK, oldBrand);
        }


        // <-- Create API Section -->
        [HttpPost]
        public async Task<IActionResult> Create(Brand newBrand)
        {
            newBrand.CreatedDate = DateTime.Now;
            newBrand.UpdatedDate = DateTime.Now;

            await _db.Brands.AddAsync(newBrand);
            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, newBrand);
        }

        //<-- Update API Section -->
        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Update(int Id, string name)
        {
            if (Id < 0 && Id == null) return BadRequest();
            Brand oldBrand = await _db.Brands.FirstOrDefaultAsync(x => x.Id == Id);
            if (oldBrand == null) return NotFound();

            oldBrand.Name = name;
            oldBrand.CreatedDate = oldBrand.CreatedDate;
            oldBrand.UpdatedDate = DateTime.Now;

            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, oldBrand);
        }


    }
}
