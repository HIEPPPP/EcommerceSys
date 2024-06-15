namespace Ecommerce.API.Models.DTO
{
    public class UpdateCategoryRequestDto
    {
        public string Name { get; set; } = null!;

        public string? Desc { get; set; } = null!;
    }
}
