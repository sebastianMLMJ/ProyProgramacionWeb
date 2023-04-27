using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoModels.Models;

public partial class Municipio
{
    public int IdMunicipio { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public int? IdDepartamento { get; set; }

    public virtual ICollection<Contact> Contacts { get; } = new List<Contact>();

    public virtual Departamento? IdDepartamentoNavigation { get; set; }
}
