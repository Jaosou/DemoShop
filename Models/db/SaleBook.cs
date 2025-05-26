using System;
using System.Collections.Generic;

namespace DemoShop.Models.db;

public partial class SaleBook
{
    public int SaleBookId { get; set; }

    public int SaleId { get; set; }

    public string BookId { get; set; } = null!;

    public int Quantity { get; set; }

    public double Price { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
