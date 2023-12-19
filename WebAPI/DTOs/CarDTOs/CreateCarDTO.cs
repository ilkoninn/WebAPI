namespace WebAPI.DTOs.CarDTOs
{
    public class CreateCarDTO
    {
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public int ColorId { get; set; }
        public int BrandId { get; set; }
    }
}
