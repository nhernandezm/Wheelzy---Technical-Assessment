using System;
using System.Collections.Generic;

namespace CarSalesDataAccessLayer.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public string? ModelName { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
