using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models.Domain;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public decimal Total { get; set; }

    public int PaymentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual PaymentDetail Payment { get; set; } = null!;
}
