using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProyectoModels.Models;

namespace ProyectoModels.ViewModels
{
    public class ProductWithImage
    {

        public Product product {get; set; }

        public IFormFile photo { get; set; }


    }
}
