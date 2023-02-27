using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Contact
{
    public int IdContact { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? HomeAddress { get; set; }

    public int? IdUser { get; set; }

    public int? IdMunicipio { get; set; }

    public virtual Municipio? IdMunicipioNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
