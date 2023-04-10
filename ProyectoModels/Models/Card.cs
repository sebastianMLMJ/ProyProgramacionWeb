﻿using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class Card
{
    public int IdCard { get; set; }

    public string Cardtype { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string ExpMonth { get; set; } = null!;

    public string ExpYear { get; set; } = null!;

    public int? IdUser { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<OrderHeader> OrderHeaders { get; } = new List<OrderHeader>();
}
