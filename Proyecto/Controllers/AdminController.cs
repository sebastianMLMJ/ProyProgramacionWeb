using Microsoft.AspNetCore.Mvc;
using ProyectoModels.ViewModels;

namespace Proyecto.Controllers
{
    public class AdminController : Controller
    {
        string url = ApiUrl.url;
        public async Task<IActionResult> AdminHome()
        {
            HttpClient client= new HttpClient();
            var kpis = await client.GetFromJsonAsync<Kpis>(url + "api/Admin");
            return View(kpis);
        }
    }
}
