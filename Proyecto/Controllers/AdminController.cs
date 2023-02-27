using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminHome()
        {
            return View();
        }
    }
}
