using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class OrderHeader
{
    public int IdOrder { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? OrderStatus { get; set; }

    public string? Total { get; set; }

    public int? IdCard { get; set; }

    public int? IdContact { get; set; }

    public virtual Card? IdCardNavigation { get; set; }

    public virtual Contact? IdContactNavigation { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
}
