using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Desc { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string? Image { get; set; }

        public string Category { get; set; }

        public int CategoryId { get; set; }

        public int? DiscountId { get; set; } = null!;

        public string Sku { get; set; }

        public decimal? DiscountPercent  {get; set;}

    }
}
