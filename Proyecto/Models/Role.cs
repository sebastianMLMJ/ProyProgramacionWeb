using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
