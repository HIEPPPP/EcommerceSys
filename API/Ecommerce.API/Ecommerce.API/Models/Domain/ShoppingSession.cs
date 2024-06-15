using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models.Domain;

public partial class ShoppingSession
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public decimal Total { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User User { get; set; } = null!;
}
