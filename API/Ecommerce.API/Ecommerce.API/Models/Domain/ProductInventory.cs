using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models.Domain;

public partial class ProductInventory
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public virtual Product? Product { get; set; }
}
