using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoModels.Models;

public partial class OrderHeader
{
    public int IdOrder { get; set; }

    public DateTime? OrderDate { get; set; }
    [MaxLength(50)]
    public string? OrderStatus { get; set; }
    [MaxLength(50)]

    public string? Total { get; set; }

    public int? IdCard { get; set; }

    public int? IdContact { get; set; }

    public virtual Card? IdCardNavigation { get; set; }

    public virtual Contact? IdContactNavigation { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
}
