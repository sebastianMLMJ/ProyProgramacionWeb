using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoModels.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Municipio> Municipios { get; } = new List<Municipio>();
}
