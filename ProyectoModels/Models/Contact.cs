using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoModels.Models;

public partial class Contact
{
    public int IdContact { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;
    [MaxLength(50)]
    public string LastName { get; set; } = null!;
    
    [MaxLength(8), MinLength(8)]
    public string PhoneNumber { get; set; } = null!;
    [MaxLength(50)]

    public string HomeAddress { get; set; } = null!;

    public int? IdUser { get; set; }

    public int? IdMunicipio { get; set; }

    public virtual Municipio? IdMunicipioNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<OrderHeader> OrderHeaders { get; } = new List<OrderHeader>();
}
