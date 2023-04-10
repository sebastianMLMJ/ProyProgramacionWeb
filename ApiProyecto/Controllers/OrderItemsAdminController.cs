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
    public class OrderItemsAdminController : ControllerBase
    {
        private readonly StoreContext _context = new StoreContext();

        // GET: api/OrderItemsAdmin
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems(int id)
        {
          if (_context.OrderItems == null)
          {
              return NotFound();
          }
            return await _context.OrderItems.Include(p=>p.IdProductNavigation).Where(p => p.IdOrder ==id).ToListAsync();
        }
    }
}
