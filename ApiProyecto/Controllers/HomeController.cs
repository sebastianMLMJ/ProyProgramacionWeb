using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        StoreContext _context = new StoreContext();

        [HttpGet("{email}")]
        public async Task<User> GetUser(string email)
        {
            var user = await _context.Users.Where(p => p.Email == email).FirstOrDefaultAsync();
            return user;
        }

        [HttpGet]
        public async Task<int> CountUser(string email)
        {
            var verifyuser = _context.Users.Count(x => x.Email == email);
            return verifyuser;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                return BadRequest();
            }
            
            return Ok();
        }
       
    }
}
