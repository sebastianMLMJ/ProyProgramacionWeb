using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoModels.Models;
using ProyectoModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using MySqlX.XDevAPI;

namespace Proyecto.Controllers
{
    public class ClientController : Controller
    {
        public string url = ApiUrl.url;

        [HttpGet]
        public async Task<IActionResult> ClientHome()
        {

            HttpClient client = new HttpClient();
            var products = await client.GetFromJsonAsync<IEnumerable<Product>>(url + "api/Client");
            return View(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            HttpClient client = new HttpClient();
            var product = await client.GetFromJsonAsync<Product>(url + "api/Client/"+ id.ToString());
            ViewBag.user = HttpContext.Session.GetString("iduser");

            return View(product);
        }
        //direcciones
        
        #region
        public async Task<IActionResult> addresses()
        {
            string? iduser = HttpContext.Session.GetString("iduser");
            IEnumerable<Contact>? list;
            try
            {
                HttpClient client = new HttpClient();
                list = await client.GetFromJsonAsync<IEnumerable<Contact>>(url + "api/Contacts?id=" + iduser);
            }
            catch (Exception)
            {
                return BadRequest();
               
            }
           
            return View(list);
        }


        public async Task<IActionResult> addressesCreate()
        {
            HttpClient client = new HttpClient();
            var contactInfo = await client.GetFromJsonAsync<ContactInfo>(url + "api/Contacts/Municipios");
            return View(contactInfo);
        }

        [HttpPost]
        public async Task<IActionResult> addressesCreate(ContactInfo newcontact, string municipios)
        {
            HttpClient client = new HttpClient();

            if (ModelState.IsValid)
            {
                newcontact.Contact.IdUser = Convert.ToInt32(HttpContext.Session.GetString("iduser"));
                newcontact.Contact.IdMunicipio = Convert.ToInt32(municipios);
                var response = await client.PostAsJsonAsync(url + "api/Contacts", newcontact.Contact);
            }
            else
            {
                var list = await client.GetFromJsonAsync<ContactInfo>(url + "api/Contacts/Municipios");
                newcontact.municipios = list.municipios;
                return View(newcontact);
            }
            
            return RedirectToAction("addresses");

        }

        public async Task<IActionResult> addressesEdit(int id)
        {

            HttpClient client = new HttpClient();
            ContactInfo? contactInfo = await client.GetFromJsonAsync<ContactInfo>(url + "api/Contacts/MunicipiosEdit?id=" + id.ToString());
            return View(contactInfo);
        }

        [HttpPost]
        public async Task <IActionResult> addressesEdit(ContactInfo contactInfo, string municipios)
        {
            HttpClient client = new HttpClient();

            if (ModelState.IsValid)
            {
                contactInfo.Contact.IdMunicipio = Convert.ToInt32(municipios);
                var response = await client.PutAsJsonAsync(url + "api/Contacts/" + contactInfo.Contact.IdContact.ToString(), contactInfo.Contact);

            }
            else
            {
                contactInfo = await client.GetFromJsonAsync<ContactInfo>(url + "api/Contacts/MunicipiosEdit?id=" + contactInfo.Contact.IdContact.ToString());
                return View(contactInfo);
            }
            

            return RedirectToAction("addresses");
        }

        public async Task <IActionResult> addressesDelete(int id)
        {
            ContactInfo contactInfo = new ContactInfo();

            HttpClient client = new HttpClient();
            contactInfo.Contact = await client.GetFromJsonAsync<Contact>(url + "api/Contacts/"+ id.ToString());

            return View(contactInfo);
        }

        [HttpPost]
        public async Task<IActionResult> addressesDelete(ContactInfo contactInfo)
        {
            Contact contact = contactInfo.Contact;
            HttpClient client = new HttpClient();

            await client.DeleteFromJsonAsync<Contact>(url + "api/Contacts/" + contactInfo.Contact.IdContact.ToString());
            return RedirectToAction("addresses");
        }
        #endregion

        #region 
        public async Task<IActionResult> Card()
        {
            string? iduser = HttpContext.Session.GetString("iduser");

            HttpClient client = new HttpClient();

            var lst = await client.GetFromJsonAsync<IEnumerable<Card>>(url + "api/Cards?id="+ iduser);
            return View(lst);
        }

        public IActionResult CardCreate()
        {
            string? iduser = HttpContext.Session.GetString("iduser");
            Card card = new Card();
            card.IdUser = Convert.ToInt32(iduser);
            return View(card);
        }

        [HttpPost]
        public async Task<IActionResult> CardCreate(Card card)
        {

            HttpClient client = new HttpClient();

            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync(url + "api/Cards", card);

            }
            else
            {
                return View(card);
            }


            return RedirectToAction("Card");
        }

        public async Task<IActionResult> CardUpdate(int id) {

           
            Card card;
            HttpClient client = new HttpClient();
            try
            {
                card = await client.GetFromJsonAsync<Card>(url + "api/Cards/" + id.ToString());

            }
            catch (Exception)
            {
                return BadRequest();
            }
            return View(card);
        }

        [HttpPost]
        public async Task<IActionResult> CardUpdate(Card card)
        {
            HttpClient client = new HttpClient();

            if (ModelState.IsValid)
            {
                var response = await client.PutAsJsonAsync(url + "api/Cards/" + card.IdCard.ToString(), card);

            }
            else
            {
                return View(card);
            }

            return RedirectToAction("Card");

        }

        public async Task<IActionResult> CardDelete(int id)
        {

            Card card;
            HttpClient client = new HttpClient();
            try
            {
                card = await client.GetFromJsonAsync<Card>(url + "api/Cards/" + id.ToString());

            }
            catch (Exception)
            {
                return BadRequest();
            }
            

            return View(card); 
        }

        [HttpPost]
        public async Task<IActionResult> CardDelete(Card card)
        {
            HttpClient client= new HttpClient();
            await client.DeleteFromJsonAsync<Card>(url +"api/Cards/" + card.IdCard.ToString());

            return RedirectToAction("Card");
        }
        #endregion
    }
}
