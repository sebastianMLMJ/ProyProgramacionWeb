using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class Card
{
    public int IdCard { get; set; }

    public string? Cardtype { get; set; }

    public string? Number { get; set; }

    public string? ExpMonth { get; set; }

    public string? ExpYear { get; set; }

    public int? IdUser { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
