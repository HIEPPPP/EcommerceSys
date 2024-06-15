using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models.Domain;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Desc { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
