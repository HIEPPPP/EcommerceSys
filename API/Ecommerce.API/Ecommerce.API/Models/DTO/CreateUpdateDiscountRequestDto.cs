namespace Ecommerce.API.Models.DTO
{
    public class CreateUpdateDiscountRequestDto
    {
        public string? Name { get; set; } = null!;

        public string? Desc { get; set; }

        public decimal DiscountPercent { get; set; }

        public bool Active { get; set; }
    }
}
