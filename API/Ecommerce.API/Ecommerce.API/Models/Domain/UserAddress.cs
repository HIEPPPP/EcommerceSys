using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models.Domain;

public partial class UserAddress
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public string? City { get; set; } = null!;

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? Telephone { get; set; } = null!;

    public string? Mobile { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
