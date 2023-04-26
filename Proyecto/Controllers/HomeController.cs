using Microsoft.AspNetCore.Mvc;
using ProyectoModels.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using BCrypt;

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
            using (var context = new StoreContext())
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                bool verify = BCrypt.Net.BCrypt.Verify(password, "$2a$11$LDniSkB218VfCZy5vnK5y.CeZMknGgDmH.cRquveVSw61rETnBffG");
                var user = await client.GetFromJsonAsync<User>(url +"api/Home/" + email);
                                    
                if (user != null && user.Password == password && user.IdRole == 1)
                {

                    HttpContext.Session.SetString("UserType", "Admin");
                    HttpContext.Session.SetString("iduser", user.IdUser.ToString());

                    return RedirectToAction("AdminHome", "Admin");
                }
                else if (user != null && user.Password == password && user.IdRole == 3)
                {
                    HttpContext.Session.SetString("iduser", user.IdUser.ToString());

                    return RedirectToAction("ClientHome", "Client");
                }
                else
                {
                    return RedirectToAction("Login", new { message = "Incorrect mail or password" });
                }
            }
            
            return View();
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

            using (var context = new StoreContext())
            {
                var verifyuser = await client.GetFromJsonAsync<int>(url + "api/Home?email=" + email);
                if (verifyuser <=0) {
                    User user = new User() { Email = email, Password= password, IdRole= 3};
                    var response = await client.PostAsJsonAsync(url + "api/Home", user);
                }
                else
                {
                    return RedirectToAction("Register",new {message = "That mail was already registered"}) ;
                }
            }
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}