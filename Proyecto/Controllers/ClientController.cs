using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Models;
using Proyecto.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Proyecto.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult ClientHome()
        {
            return View();
        }
        //direcciones
        #region
        public IActionResult addresses()
        {
            int iduser = Convert.ToInt32(HttpContext.Session.GetString("iduser"));

            List<ContactInfo> list;
            //List<Contact> list;
            using (var context = new StoreContext())
            {
                list =
                (from c in context.Contacts
                 join m in context.Municipios on c.IdMunicipio equals m.IdMunicipio
                 join d in context.Departamentos on m.IdDepartamento equals d.IdDepartamento
                 select new ContactInfo
                 {
                     Contact = c,
                     Municipio = m,
                     Departamento = d
                 }).ToList();

            }

            return View(list);
        }

        public IActionResult addressesCreate()
        {
            ContactInfo contactInfo = new ContactInfo();
            List<Municipio> municipios = new List<Municipio>();
            using (var context = new StoreContext())
            {
                municipios =
               (from d in context.Municipios
                select new Municipio
                { Name = d.Name,
                    IdMunicipio = d.IdMunicipio }).ToList();
            }

            contactInfo.municipios = municipios.ConvertAll(d => {

                return new SelectListItem
                {
                    Text = d.Name,
                    Value = d.IdMunicipio.ToString(),
                    Selected = false
                };
            });

            return View(contactInfo);
        }

        [HttpPost]
        public IActionResult addressesCreate(ContactInfo newcontact, string municipios)
        {
            newcontact.Contact.IdUser = Convert.ToInt32(HttpContext.Session.GetString("iduser"));
            newcontact.Contact.IdMunicipio = Convert.ToInt32(municipios);
            using (var context = new StoreContext())
            {
                context.Contacts.Add(newcontact.Contact);
                context.SaveChanges();
            }

            return RedirectToAction("addresses");

        }

        public IActionResult addressesEdit(int id)
        {
            ContactInfo contactInfo = new ContactInfo();
            List<Municipio> municipios = new List<Municipio>();

            using (var context = new StoreContext())
            {
                contactInfo.Contact = context.Contacts.Where(x => x.IdContact == id).FirstOrDefault();
                municipios =
                    (from d in context.Municipios
                     select new Municipio
                     {
                         Name = d.Name,
                         IdMunicipio = d.IdMunicipio
                     }).ToList();
            }

            contactInfo.municipios = municipios.ConvertAll(d => {

                return new SelectListItem
                {
                    Text = d.Name,
                    Value = d.IdMunicipio.ToString(),
                    Selected = true,

                };

            });

            foreach (var item in contactInfo.municipios)
            {
                if (item.Value == contactInfo.Contact.IdMunicipio.ToString())
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }


            return View(contactInfo);
        }

        [HttpPost]
        public IActionResult addressesEdit(ContactInfo contactInfo, string municipios)
        {
            contactInfo.Contact.IdMunicipio = Convert.ToInt32(municipios);

            using (var context = new StoreContext())
            {
                context.Contacts.Update(contactInfo.Contact);
                context.SaveChanges();

            }
            return RedirectToAction("addresses");
        }

        public IActionResult addressesDelete(int id)
        {
            ContactInfo contactInfo = new ContactInfo();

            using (var context = new StoreContext())
            {
                contactInfo.Contact = context.Contacts.Where(x => x.IdContact == id).FirstOrDefault();
            }

            return View(contactInfo);
        }

        [HttpPost]
        public IActionResult addressesDelete(ContactInfo contactInfo)
        {
            Contact contact = contactInfo.Contact;
            using (var context = new StoreContext())
            {
                context.Contacts.Remove(contact);
                context.SaveChanges();
            }
            return RedirectToAction("addresses");
        }
#endregion

        public IActionResult Card()
        {
            List<Card> list = new List<Card>();
            using(var context = new StoreContext())
            {
                list = (from c in context.Cards select new Card 
                {
                    IdUser = c.IdUser,
                    IdCard= c.IdCard,
                    Cardtype= c.Cardtype,
                    Number= c.Number,
                    ExpMonth= c.ExpMonth,
                    ExpYear= c.ExpYear
                
                }).ToList();
            }
            return View(list);
        }

        public IActionResult CardCreate()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult CardCreate(Card card)
        {

            card.IdUser = Convert.ToInt32(HttpContext.Session.GetString("iduser"));
            using (var context = new StoreContext())
            {
                context.Cards.Add(card);
                context.SaveChanges();
            }

            return RedirectToAction("Card");
        }

        public IActionResult CardUpdate(int id) {

            Card card = new Card();
            using(var context = new StoreContext()) {
                card = context.Cards.Where(x => x.IdCard == id).FirstOrDefault();
            }
            return View(card);
        }

        [HttpPost]
        public IActionResult CardUpdate(Card card)
        {
            using(var context = new StoreContext())
            {
                context.Cards.Update(card);
                context.SaveChanges();
            }
            return RedirectToAction("Card");

        }

        public IActionResult CardDelete(int id)
        {
            Card card = new Card();
            using (var context = new StoreContext())
            {
                card = context.Cards.Where(x=> x.IdCard== id).FirstOrDefault();
            }

            return View(card); 
        }

        [HttpPost]
        public IActionResult CardDelete(Card card)
        {
            using(var context = new StoreContext())
            {
                context.Cards.Remove(card);
                context.SaveChanges();
            }
            return RedirectToAction("Card");
        }
    }
}
