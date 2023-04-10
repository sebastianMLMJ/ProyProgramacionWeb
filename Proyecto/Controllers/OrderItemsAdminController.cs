using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;

namespace Proyecto.Controllers
{
    public class OrderItemsAdminController : Controller
    {

        string url = ApiUrl.url;
        // GET: OrderItemsAdmin
        public async Task<IActionResult> Index(int id)
        {
            HttpClient client= new HttpClient();
            var items = await client.GetFromJsonAsync<IEnumerable<OrderItem>>(url+"api/OrderItemsAdmin/" + id.ToString());
            return View(items);
        }

       
    }
}
