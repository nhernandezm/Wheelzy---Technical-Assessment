using System;
using System.Collections.Generic;

namespace CarSalesDataAccessLayer.Models;

public partial class Costumer
{
    public int CostumerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int LocationId { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual Location Location { get; set; } = null!;
}
