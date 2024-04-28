using System;
using System.Collections.Generic;

namespace CarSalesDataAccessLayer.Models;

public partial class SubModel
{
    public int SubModelId { get; set; }

    public string? SubModelName { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
