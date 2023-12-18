
namespace WebAPI.Models
{
    public class Color : BaseAuditableEntity
    {
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
