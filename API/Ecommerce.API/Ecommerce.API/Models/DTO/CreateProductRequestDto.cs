using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Models.DTO
{
    public class CreateProductRequestDto
    {
        public string Name { get; set; } = null!;

        public string? Desc { get; set; }

        public string Sku { get; set; } = null!;

        public int CategoryId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int? DiscountId { get; set; } = null!;

        public string? Image { get; set; }

    }
}
