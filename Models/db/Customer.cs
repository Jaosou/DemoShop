using System;
using System.Collections.Generic;

namespace DemoShop.Models.db;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
