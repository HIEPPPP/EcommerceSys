using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user);

        string CreateRefreshToken();

        ClaimsPrincipal GetTokenPrincipal(string jwtToken); 
        
    }
}
