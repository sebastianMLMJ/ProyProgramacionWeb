using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoModels.Models;
using System.Text.RegularExpressions;

namespace Proyecto.Controllers
{
    public class ShoppingCartController : Controller
    {
        string url = ApiUrl.url;

        //GET: ShoppingCart
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            string? clientid = HttpContext.Session.GetString("iduser");
            var cart = await client.GetFromJsonAsync<IEnumerable<Shoppingcart>>(url + "api/ShoppingCart/" +clientid );

            double total = 0;
            foreach (var item in cart)
            {
                var stripped = Regex.Replace(item.IdProductNavigation.Price, "[a-zA-Z]", "");
                total = total + Convert.ToDouble(stripped);
            }
            ViewBag.total = total;
            return View(cart);
        }



        // POST: ShoppingCartController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Shoppingcart shoppingcart)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync(url + "api/ShoppingCart", shoppingcart);
            }
           
            return RedirectToAction(nameof(Index));
            
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteFromJsonAsync<Shoppingcart>(url + "api/ShoppingCart/" + id.ToString());

            return RedirectToAction(nameof(Index));
        }

        
    }
}
