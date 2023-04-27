using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoModels.Models;

public partial class Card
{
    public int IdCard { get; set; }
    [MaxLength(20)]
    public string Cardtype { get; set; } = null!;
    
    [MaxLength(16), MinLength(16)]
    public string Number { get; set; } = null!;

    [MaxLength(2)]
    public string ExpMonth { get; set; } = null!;

    [MaxLength(2)]
    public string ExpYear { get; set; } = null!;

    public int? IdUser { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<OrderHeader> OrderHeaders { get; } = new List<OrderHeader>();
}
