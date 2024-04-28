using System;
using System.Collections.Generic;

namespace CarSalesDataAccessLayer.Models;

public partial class Log
{
    public int LogId { get; set; }

    public string? LogMessage { get; set; }

    public DateTime? LogDate { get; set; }

    public int? PotentialBuyerId { get; set; }

    public int? StatusId { get; set; }

    public virtual PotentialBuyer? PotentialBuyer { get; set; }

    public virtual Status? Status { get; set; }
}
