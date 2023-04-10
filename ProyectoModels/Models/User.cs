using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdRole { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();

    public virtual ICollection<Contact> Contacts { get; } = new List<Contact>();

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<Shoppingcart> Shoppingcarts { get; } = new List<Shoppingcart>();
}
