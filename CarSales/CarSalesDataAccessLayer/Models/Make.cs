using System;
using System.Collections.Generic;

namespace CarSalesDataAccessLayer.Models;

public partial class Make
{
    public int MakeId { get; set; }

    public string? MakeName { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
