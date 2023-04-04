using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class Contact
{
    public int IdContact { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string HomeAddress { get; set; } = null!;

    public int? IdUser { get; set; }

    public int? IdMunicipio { get; set; }

    public virtual Municipio? IdMunicipioNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
