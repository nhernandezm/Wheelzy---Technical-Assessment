using System;
using System.Collections.Generic;

namespace CarSalesDataAccessLayer.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string? LocationName { get; set; }

    public string? ZipCode { get; set; }

    public virtual ICollection<Buyer> Buyers { get; set; } = new List<Buyer>();

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Costumer> Costumers { get; set; } = new List<Costumer>();
}
