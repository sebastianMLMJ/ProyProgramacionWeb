using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHeadersController : ControllerBase
    {
        private readonly StoreContext _context = new StoreContext();

        // GET: api/OrderHeaders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderHeader>>> GetOrderHeaders(int id)
        {
          if (_context.OrderHeaders == null)
          {
              return NotFound();
          }
            return await _context.OrderHeaders.Where(p => p.IdContactNavigation.IdUser == id).ToListAsync();
        }

        // GET: api/OrderHeaders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderHeader>> GetOrderHeader(int id)
        {
          if (_context.OrderHeaders == null)
          {
              return NotFound();
          }
            var orderHeader = await _context.OrderHeaders.FindAsync(id);

            if (orderHeader == null)
            {
                return NotFound();
            }

            return orderHeader;
        }

        // PUT: api/OrderHeaders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderHeader(int id, OrderHeader orderHeader)
        {
            if (id != orderHeader.IdOrder)
            {
                return BadRequest();
            }

            _context.Entry(orderHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderHeaderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderHeaders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderHeader>> PostOrderHeader(OrderHeader orderHeader, int id)
        {
          if (_context.OrderHeaders == null)
          {
              return Problem("Entity set 'StoreContext.OrderHeaders'  is null.");
          }
          if (ModelState.IsValid)
            {

            }
            _context.Database.BeginTransaction();
            try
            {
                _context.OrderHeaders.Add(orderHeader);

                await _context.SaveChangesAsync();

                var shoppingcart = await _context.Shoppingcarts.Where(p => p.IdUser == id).ToListAsync();

                List<OrderItem> orderItems = new List<OrderItem>();
                List<Product> orderproducts = new List<Product>();
                foreach (var item in shoppingcart)
                {
                    OrderItem newItem = new OrderItem() { IdOrder = orderHeader.IdOrder, IdProduct = item.IdProduct };
                    Product product = await _context.Products.FirstAsync(p => p.IdProduct== item.IdProduct);
                    product.Stock = product.Stock - 1;
                    orderproducts.Add(product);
                    orderItems.Add(newItem);

                }

                _context.Products.UpdateRange(orderproducts);
                
                _context.OrderItems.AddRange(orderItems);

                _context.RemoveRange(shoppingcart);
              
                _context.Database.CommitTransaction();
                await _context.SaveChangesAsync();
                

            }
            catch (Exception e)
            {
                _context.Database.RollbackTransaction();
            }
            
            return CreatedAtAction("GetOrderHeader", new { id = orderHeader.IdOrder }, orderHeader);
        }

        // DELETE: api/OrderHeaders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderHeader(int id)
        {
            if (_context.OrderHeaders == null)
            {
                return NotFound();
            }
            var orderHeader = await _context.OrderHeaders.FindAsync(id);
            if (orderHeader == null)
            {
                return NotFound();
            }

            _context.OrderHeaders.Remove(orderHeader);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderHeaderExists(int id)
        {
            return (_context.OrderHeaders?.Any(e => e.IdOrder == id)).GetValueOrDefault();
        }
    }
}
