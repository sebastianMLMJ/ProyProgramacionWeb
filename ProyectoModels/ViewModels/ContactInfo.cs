using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoModels.Models;

namespace ProyectoModels.ViewModels
{
    public class ContactInfo
    {
        public Contact? Contact { get; set; }
        public List<SelectListItem>? municipios { get; set; }
        public Departamento? Departamento { get; set; }
        public Municipio? Municipio { get; set; }

    }
}
