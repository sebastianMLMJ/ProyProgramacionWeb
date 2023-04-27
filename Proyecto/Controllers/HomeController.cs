using Microsoft.AspNetCore.Mvc;
using ProyectoModels.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using BCrypt;
using System.IdentityModel.Tokens.Jwt;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        string url = ApiUrl.url;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string? message)
        {
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, bool checkbox)
        {
            HttpClient client = new HttpClient();

            var token = await client.GetFromJsonAsync<Token>(url +"api/Home/" + email + "/" + password);
            string iduser;
            string idrole;
            if (token.token != null)
            {
                var readedtoken = new JwtSecurityTokenHandler().ReadJwtToken(token.token);
                 iduser = readedtoken.Payload["id"].ToString();
                 idrole = readedtoken.Payload["idRole"].ToString();

                if (idrole == "1")
                {
                    HttpContext.Session.SetString("UserType", "Admin");
                    HttpContext.Session.SetString("iduser", iduser);

                    return RedirectToAction("AdminHome", "Admin");
                }
                else if (idrole == "3")
                {
                    HttpContext.Session.SetString("iduser", iduser);
                    return RedirectToAction("ClientHome", "Client");
                }
                else
                {
                    return RedirectToAction("Login", new { message = "Incorrect mail or password" });
                }

            }

            return RedirectToAction("Login", new { message = "Incorrect mail or password" });
        }

        public IActionResult Register(string? message)
        {
            ViewBag.message = message;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {

            HttpClient client = new HttpClient();
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            using (var context = new StoreContext())
            {
                var verifyuser = await client.GetFromJsonAsync<int>(url + "api/Home?email=" + email);
                if (verifyuser <=0) {
                    User user = new User() { Email = email, Password= encryptedPassword, IdRole= 3};
                    var response = await client.PostAsJsonAsync(url + "api/Home", user);
                }
                else
                {
                    return RedirectToAction("Register",new {message = "That mail was already registered"}) ;
                }
            }
            return RedirectToAction(nameof(Login));
        }
    }
}