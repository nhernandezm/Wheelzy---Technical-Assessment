using System;
using System.Collections.Generic;

namespace CarSalesDataAccessLayer.Models;

public partial class Car
{
    public int CarId { get; set; }

    public int? MakeId { get; set; }

    public int? ModelId { get; set; }

    public int? SubModelId { get; set; }

    public int? LocationId { get; set; }

    public int? CostumerId { get; set; }

    public string RegistrationNumber { get; set; } = null!;

    public int? Year { get; set; }

    public decimal? Price { get; set; }

    public virtual Costumer? Costumer { get; set; }

    public virtual Location? Location { get; set; }

    public virtual Make? Make { get; set; }

    public virtual Model? Model { get; set; }

    public virtual ICollection<PotentialBuyer> PotentialBuyers { get; set; } = new List<PotentialBuyer>();

    public virtual SubModel? SubModel { get; set; }
}
