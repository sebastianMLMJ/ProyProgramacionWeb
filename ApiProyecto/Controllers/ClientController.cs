using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        StoreContext _context = new StoreContext();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await (from p in _context.Products
                                  where p.Stock > 0
                            select new Product
                            {
                                IdProduct = p.IdProduct,
                                Name= p.Name,
                                Price= p.Price,
                                Photo = p.Photo,
                            }).ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.Where(p => p.IdProduct== id).FirstAsync();
            return product;
        }
    }
}
