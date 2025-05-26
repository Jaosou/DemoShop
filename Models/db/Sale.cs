using System;
using System.Collections.Generic;

namespace DemoShop.Models.db;

public partial class Sale
{
    public int SaleId { get; set; }

    public DateTime SaleDate { get; set; }

    public double TotalAmount { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<SaleBook> SaleBooks { get; set; } = new List<SaleBook>();
}
