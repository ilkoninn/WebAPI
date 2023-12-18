
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CarsController(AppDbContext db)
        {
            _db = db;
        }

        // <-- Get API Section -->
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Car> Cars = await _db.Cars.ToListAsync();

            return StatusCode(StatusCodes.Status200OK, Cars);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id < 0 && Id == null) return BadRequest();
            Car oldCar = await _db.Cars.FirstOrDefaultAsync(x => x.Id == Id);
            if (oldCar == null) return NotFound();


            return StatusCode(StatusCodes.Status200OK, oldCar);
        }


        // <-- Create API Section -->
        [HttpPost]
        public async Task<IActionResult> Create(Car newCar)
        {
            newCar.CreatedDate = DateTime.Now;
            newCar.UpdatedDate = DateTime.Now;

            await _db.Cars.AddAsync(newCar);
            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, newCar);
        }

        //<-- Update API Section -->
        public async Task<IActionResult> Update(int Id, int modelYear, 
            decimal dailyPrice, string description, int colorId, int brandId)
        {
            if (Id < 0 && Id == null) return BadRequest();
            Car oldCar = await _db.Cars.FirstOrDefaultAsync(x => x.Id == Id);
            if (oldCar == null) return NotFound();

            oldCar.ModelYear = modelYear;
            oldCar.Description = description;
            oldCar.ColorId = colorId;
            oldCar.BrandId = brandId;
            oldCar.DailyPrice = dailyPrice;
            oldCar.CreatedDate = oldCar.CreatedDate;
            oldCar.UpdatedDate = DateTime.Now;

            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, oldCar);
        }
    }
}
