using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class OrderItem
{
    public int IdOrderItem { get; set; }

    public int? IdProduct { get; set; }

    public int? IdOrder { get; set; }

    public virtual OrderHeader? IdOrderNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
