using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models.Domain;

public partial class UserPayment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string PaymentType { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public int AccountNo { get; set; }

    public string Expiry { get; set; }

    public virtual User User { get; set; } = null!;
}
