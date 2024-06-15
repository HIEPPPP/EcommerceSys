using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models.Domain;

public partial class PaymentDetail
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public string? Provider { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual OrderDetail? OrderDetail { get; set; }
}
