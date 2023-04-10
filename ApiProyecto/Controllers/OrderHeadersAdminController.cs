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
    public class OrderHeadersAdminController : ControllerBase
    {
        private readonly StoreContext _context = new StoreContext();

        // GET: api/OrderHeadersAdmin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderHeader>>> GetOrderHeaders()
        {
          if (_context.OrderHeaders == null)
          {
              return NotFound();
          }
            return await _context.OrderHeaders.Include(p => p.IdCardNavigation).Include(p=>p.IdContactNavigation).Include(p => p.IdContactNavigation.IdMunicipioNavigation).Include(p=> p.IdContactNavigation.IdMunicipioNavigation.IdDepartamentoNavigation).ToListAsync();
        }

        // GET: api/OrderHeadersAdmin/5
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

        // PUT: api/OrderHeadersAdmin/5
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
        private bool OrderHeaderExists(int id)
        {
            return (_context.OrderHeaders?.Any(e => e.IdOrder == id)).GetValueOrDefault();
        }
    }
}
