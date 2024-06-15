namespace Ecommerce.API.Models.DTO
{
    public class DiscountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Desc { get; set; }

        public decimal DiscountPercent { get; set; }

        public bool Active { get; set; }
    }
}
