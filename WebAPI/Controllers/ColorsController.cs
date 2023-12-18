using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ColorsController(AppDbContext db)
        {
            _db = db;
        }

        // <-- Get API Section -->
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Color> Colors = await _db.Colors.ToListAsync();

            return StatusCode(StatusCodes.Status200OK, Colors);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id < 0 && Id == null) return BadRequest();
            Color oldColor = await _db.Colors.FirstOrDefaultAsync(x => x.Id == Id);
            if (oldColor == null) return NotFound();


            return StatusCode(StatusCodes.Status200OK, oldColor);
        }


        // <-- Create API Section -->
        [HttpPost]
        public async Task<IActionResult> Create(Color newColor)
        {
            newColor.CreatedDate = DateTime.Now;
            newColor.UpdatedDate = DateTime.Now;

            await _db.Colors.AddAsync(newColor);
            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, newColor);
        }

        //<-- Update API Section -->
        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Update(int Id, string name)
        {
            if (Id < 0 && Id == null) return BadRequest();
            Color oldColor = await _db.Colors.FirstOrDefaultAsync(x => x.Id == Id);
            if (oldColor == null) return NotFound();

            oldColor.Name = name;
            oldColor.CreatedDate = oldColor.CreatedDate;
            oldColor.UpdatedDate = DateTime.Now;

            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, oldColor);
        }
    }
}
