using Microsoft.AspNetCore.Mvc;
using ProyectoModels.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        

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
        public IActionResult Login(string email, string password, bool checkbox)
        {
            using (var context = new StoreContext())
            {
                var user = context.Users.Where(x => x.Email == email).FirstOrDefault();

                HttpContext.Session.SetString("iduser", user.IdUser.ToString());
               
                if (user != null && user.Password == password && user.IdRole == 1)
                {
                    return RedirectToAction("AdminHome", "Admin");
                }
                else if (user != null && user.Password == password && user.IdRole == 3)
                {
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
        public IActionResult Register(string email, string password)
        {


            using (var context = new StoreContext())
            {
                var verifyuser = context.Users.Count(x => x.Email == email);
                if (verifyuser <=0) {
                    context.Users.Add(new User { Email = email, Password = password, IdRole = 3 });
                    context.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Register",new {message = "That mail was already registered"}) ;
                }
            }
            return View();
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