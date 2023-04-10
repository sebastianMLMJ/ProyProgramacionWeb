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
    public class OrderHeadersAdminController : Controller
    {
        private readonly StoreContext _context= new StoreContext();

        string url = ApiUrl.url;
        // GET: OrderHeadersAdmin
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var orders = await client.GetFromJsonAsync<IEnumerable<OrderHeader>>(url + "api/OrderHeadersAdmin");
            return View(orders);
        }

        // GET: OrderHeadersAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return RedirectToAction("Index", new { id });
        }

        
        // GET: OrderHeadersAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            HttpClient client = new HttpClient();
            var orderHeader = await client.GetFromJsonAsync<OrderHeader>(url + "api/OrderHeadersAdmin/" + id.ToString());
            if (orderHeader == null)
            {
                return NotFound();
            }
            
            return View(orderHeader);
        }

        // POST: OrderHeadersAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrder,OrderDate,OrderStatus,Total,IdCard,IdContact")] OrderHeader orderHeader)
        {
            HttpClient client = new HttpClient();
            if (id != orderHeader.IdOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await client.PutAsJsonAsync(url + "api/OrderHeadersAdmin/"+ id.ToString(), orderHeader);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
           
            return View(orderHeader);
        }
        private bool OrderHeaderExists(int id)
        {
          return (_context.OrderHeaders?.Any(e => e.IdOrder == id)).GetValueOrDefault();
        }
    }
}
