using System;
using System.Collections.Generic;

namespace CarSalesDataAccessLayer.Models;

public partial class Buyer
{
    public int BuyerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int LocationId { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<PotentialBuyer> PotentialBuyers { get; set; } = new List<PotentialBuyer>();
}
