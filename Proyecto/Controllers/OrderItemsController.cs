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
    public class OrderItemsController : Controller
    {
        private readonly StoreContext _context = new StoreContext();

        string url = ApiUrl.url;

        // GET: OrderItems
        public async Task<IActionResult> Index(int id)
        {
            HttpClient client = new HttpClient();
            IEnumerable<OrderItem> items;
            try
            {
                items = await client.GetFromJsonAsync<IEnumerable<OrderItem>>(url + "api/OrderItems/" + id.ToString());

            }
            catch (Exception)
            {
                return BadRequest();
            }

            return View(items);
        }
    }
}
