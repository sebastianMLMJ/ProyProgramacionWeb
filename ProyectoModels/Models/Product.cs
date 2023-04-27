using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoModels.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(300)]
    public string Description { get; set; } = null!;
    
    [MaxLength(50)]
    public string Price { get; set; } = null!;
    
    public int Stock { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual ICollection<Shoppingcart> Shoppingcarts { get; } = new List<Shoppingcart>();
}
