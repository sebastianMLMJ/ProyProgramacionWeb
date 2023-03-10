using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Municipio> Municipios { get; } = new List<Municipio>();
}
