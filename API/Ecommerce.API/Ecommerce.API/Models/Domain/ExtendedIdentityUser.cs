﻿using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Models.Domain
{
    public class ExtendedIdentityUser : IdentityUser 
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
