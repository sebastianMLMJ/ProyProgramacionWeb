using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class Municipio
{
    public int IdMunicipio { get; set; }

    public string? Name { get; set; }

    public int? IdDepartamento { get; set; }

    public virtual ICollection<Contact> Contacts { get; } = new List<Contact>();

    public virtual Departamento? IdDepartamentoNavigation { get; set; }
}
