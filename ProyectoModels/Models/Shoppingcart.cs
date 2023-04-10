using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class Shoppingcart
{
    public int IdShoppingcart { get; set; }

    public int? IdProduct { get; set; }

    public int? IdUser { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
