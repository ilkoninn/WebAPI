namespace WebAPI.DTOs.CarDTOs
{
    public class UpdateCarDTO
    {
        public int Id { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public int ColorId { get; set; }
        public int BrandId { get; set; }
    }
}
