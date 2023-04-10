using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        StoreContext _context = new StoreContext();

        // GET: api/ShoppingCart

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Shoppingcart>>> GetShoppingCart(int id)
        {
            if (_context.Shoppingcarts == null)
            {
                return NotFound();
            }
            return await _context.Shoppingcarts.Include(p => p.IdProductNavigation).Where(p=>p.IdUser==id).ToListAsync();
        }

        // POST: api/ShoppingCart
        [HttpPost]
        public async Task<ActionResult<Shoppingcart>> PostShopingCart (Shoppingcart shoppingcart)
        {

            if (_context.Shoppingcarts == null)
            {
                return Problem("Entity set 'StoreContext.ShoppingCart'  is null.");
            }
            
            _context.Shoppingcarts.Add(shoppingcart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCart", new { id = shoppingcart.IdProduct }, shoppingcart);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            if (_context.Shoppingcarts == null)
            {
                return NotFound();
            }
            var item = await _context.Shoppingcarts.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Shoppingcarts.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }
    }
}
