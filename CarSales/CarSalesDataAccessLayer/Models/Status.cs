using System;
using System.Collections.Generic;

namespace CarSalesDataAccessLayer.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<PotentialBuyer> PotentialBuyers { get; set; } = new List<PotentialBuyer>();
}
