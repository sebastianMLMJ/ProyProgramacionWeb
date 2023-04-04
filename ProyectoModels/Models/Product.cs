using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Price { get; set; } = null!;

    public int Stock { get; set; }

    public string Photo { get; set; } = null!;
}
