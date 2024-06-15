namespace Ecommerce.API.Models.Domain
{
    public class RefreshTokenModel
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
