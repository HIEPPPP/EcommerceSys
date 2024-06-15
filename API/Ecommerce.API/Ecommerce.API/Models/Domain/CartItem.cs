using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models.Domain;

public partial class CartItem
{
    public int Id { get; set; }

    public int SessionId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
        
    public virtual ShoppingSession Session { get; set; } = null!;
}
