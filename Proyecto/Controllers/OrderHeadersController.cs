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
        private readonly StoreContext _context = new StoreContext();
        string url = ApiUrl.url;
        // GET: OrderHeaders
        public async Task<IActionResult> Index()
        {
            string id = HttpContext.Session.GetString("iduser");
            HttpClient client = new HttpClient();
            var orders = await client.GetFromJsonAsync<IEnumerable<OrderHeader>>(url+ "api/OrderHeaders?id=" + id);
            return View(orders);
        }

        // GET: OrderHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderHeaders == null)
            {
                return NotFound();
            }

            var orderHeader = await _context.OrderHeaders
                .Include(o => o.IdCardNavigation)
                .Include(o => o.IdContactNavigation)
                .FirstOrDefaultAsync(m => m.IdOrder == id);
            if (orderHeader == null)
            {
                return NotFound();
            }

            return View(orderHeader);
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

        // GET: OrderHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderHeaders == null)
            {
                return NotFound();
            }

            var orderHeader = await _context.OrderHeaders.FindAsync(id);
            if (orderHeader == null)
            {
                return NotFound();
            }
            ViewData["IdCard"] = new SelectList(_context.Cards, "IdCard", "Cardtype", orderHeader.IdCard);
            ViewData["IdContact"] = new SelectList(_context.Contacts, "IdContact", "FirstName", orderHeader.IdContact);
            return View(orderHeader);
        }

        // POST: OrderHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrder,OrderDate,OrderStatus,Total,IdCard,IdContact")] OrderHeader orderHeader)
        {
            if (id != orderHeader.IdOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderHeaderExists(orderHeader.IdOrder))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCard"] = new SelectList(_context.Cards, "IdCard", "Cardtype", orderHeader.IdCard);
            ViewData["IdContact"] = new SelectList(_context.Contacts, "IdContact", "FirstName", orderHeader.IdContact);
            return View(orderHeader);
        }

        // GET: OrderHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderHeaders == null)
            {
                return NotFound();
            }

            var orderHeader = await _context.OrderHeaders
                .Include(o => o.IdCardNavigation)
                .Include(o => o.IdContactNavigation)
                .FirstOrDefaultAsync(m => m.IdOrder == id);
            if (orderHeader == null)
            {
                return NotFound();
            }

            return View(orderHeader);
        }

        // POST: OrderHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderHeaders == null)
            {
                return Problem("Entity set 'StoreContext.OrderHeaders'  is null.");
            }
            var orderHeader = await _context.OrderHeaders.FindAsync(id);
            if (orderHeader != null)
            {
                _context.OrderHeaders.Remove(orderHeader);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderHeaderExists(int id)
        {
          return (_context.OrderHeaders?.Any(e => e.IdOrder == id)).GetValueOrDefault();
        }
    }
}
