namespace Ecommerce.API.Models.DTO
{
    public class CreateUpdateUserRequestDto
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Telephone { get; set; } = null!;
    }
}
