using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;

namespace Proyecto.Controllers
{
    public class OrderHeadersController : Controller
    {
        string url = ApiUrl.url;
        // GET: OrderHeaders
        public async Task<IActionResult> Index()
        {
            string id = HttpContext.Session.GetString("iduser");
            HttpClient client = new HttpClient();
            var orders = await client.GetFromJsonAsync<IEnumerable<OrderHeader>>(url+ "api/OrderHeaders?id=" + id);
            return View(orders);
        }

        

        // GET: OrderHeaders/Create
        public async Task<IActionResult> Create(string id)
        {

            ViewData["total"] ="Q" + id;
            HttpClient client = new HttpClient();
            var userid = HttpContext.Session.GetString("iduser");
            var cards = await client.GetFromJsonAsync<IEnumerable<Card>>(url + "api/Cards?id=" + userid);
            var contacts = await client.GetFromJsonAsync<IEnumerable<Contact>>(url + "api/Contacts?id=" + userid);
            ViewData["IdCard"] = new SelectList(cards, "IdCard", "Number");
            ViewData["IdContact"] = new SelectList(contacts, "IdContact", "HomeAddress");
            return View();
        }

        // POST: OrderHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrder,OrderDate,OrderStatus,Total,IdCard,IdContact")] OrderHeader orderHeader)
        {
            orderHeader.OrderDate = DateTime.UtcNow;
            orderHeader.OrderStatus = "Por Confirmar";
            var userid = HttpContext.Session.GetString("iduser");
            HttpClient client = new HttpClient();
            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync(url + "api/OrderHeaders?id="+userid,orderHeader);
                return RedirectToAction(nameof(Index));
            }

            var cards = await client.GetFromJsonAsync<IEnumerable<Card>>(url + "api/Cards?id=" + userid);
            var contacts = await client.GetFromJsonAsync<IEnumerable<Contact>>(url + "api/Contacts?id=" + userid);
            ViewData["IdCard"] = new SelectList(cards, "IdCard", "Number");
            ViewData["IdContact"] = new SelectList(contacts, "IdContact", "HomeAddress");
            return View(orderHeader);
        }

        

        
    }
}
