
namespace WebAPI.Models
{
    public class Car : BaseAuditableEntity
    {
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public int ColorId { get; set; }
        public Color? Color { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
    }
}
