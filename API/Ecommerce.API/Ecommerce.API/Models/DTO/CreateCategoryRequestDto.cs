namespace Ecommerce.API.Models.DTO
{
    public class CreateCategoryRequestDto
    {
        public string Name { get; set; } = null!;

        public string? Desc { get; set; } = null!;
    }
}
