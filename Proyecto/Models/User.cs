using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? IdRole { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();

    public virtual ICollection<Contact> Contacts { get; } = new List<Contact>();

    public virtual Role? IdRoleNavigation { get; set; }
}
