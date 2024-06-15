using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models.Domain;

public partial class Discount
{
    public int Id { get; set; }

    public string? Name { get; set; } = null!;

    public string? Desc { get; set; }

    public decimal DiscountPercent { get; set; }

    public bool Active { get; set; }

    public byte[] CreatedAt { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
