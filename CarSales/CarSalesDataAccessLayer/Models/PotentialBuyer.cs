using System;
using System.Collections.Generic;

namespace CarSalesDataAccessLayer.Models;

public partial class PotentialBuyer
{
    public int PotentialBuyerId { get; set; }

    public int? BuyerId { get; set; }

    public decimal Amount { get; set; }

    public bool? IsCurrentOne { get; set; }

    public DateTime? DatePickup { get; set; }

    public int? CarId { get; set; }

    public int? StatusId { get; set; }

    public virtual Buyer? Buyer { get; set; }

    public virtual Car? Car { get; set; }

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual Status? Status { get; set; }
}
