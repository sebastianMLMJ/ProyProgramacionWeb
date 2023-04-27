using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoModels.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        StoreContext _context = new StoreContext();

        public IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{email}/{password}")]
        public async Task<Token> GetUser(string email,string password)
        {
            var user = await _context.Users.Where(p => p.Email == email).FirstOrDefaultAsync();
            bool verified = false;
            if (password != null && user != null) 
            {
                verified = BCrypt.Net.BCrypt.Verify(password, user.Password);


            }
            if (user != null && verified == true)
            {
                var jwt = _configuration.GetSection("JWT").Get<Jwtconfig>();
                var claims = new[]
                {
                //new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject.ToString()),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", user.IdUser.ToString()),
                new Claim("idRole", user.IdRole.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
                var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: singIn,
                    claims: claims

                    );
                Token newtoken = new Token() { token = new JwtSecurityTokenHandler().WriteToken(token) };
                return newtoken;
            }
            else
            {
                return new Token();
            }
            
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
