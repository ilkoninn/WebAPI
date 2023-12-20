
namespace WebAPI.Models
{
    public class Brand : BaseAuditableEntity
    {
        public string Name { get; set; }
        public ICollection<Car>? Cars { get; set; }
    }
}
