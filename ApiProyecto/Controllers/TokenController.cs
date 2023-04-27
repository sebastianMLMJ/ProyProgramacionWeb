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
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public Token CreateToken()
        {
            var jwt = _configuration.GetSection("JWT").Get<Jwtconfig>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
            var singIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: singIn
                );
            Token newtoken = new Token() { token = new JwtSecurityTokenHandler().WriteToken(token) };
            return newtoken;
        }


    }
}
