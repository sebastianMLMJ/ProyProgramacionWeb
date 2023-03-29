using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;
using ProyectoModels.ViewModels;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        
        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts(int id)
        {
          StoreContext _context = new StoreContext();   
          if (_context.Contacts == null)
          {
              return NotFound();
          }
            return await _context.Contacts.Include(p => p.IdMunicipioNavigation).Include(p => p.IdMunicipioNavigation.IdDepartamentoNavigation).Where(p=>p.IdUser==id).ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            StoreContext _context = new StoreContext();
            if (_context.Contacts == null)
            {
              return NotFound();
            }
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            StoreContext _context = new StoreContext();
            if (id != contact.IdContact)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            StoreContext _context = new StoreContext();
            if (_context.Contacts == null)
          {
              return Problem("Entity set 'StoreContext.Contacts'  is null.");
          }
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.IdContact }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        
        {
            StoreContext _context = new StoreContext();
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        private bool ContactExists(int id)
        {
            StoreContext _context = new StoreContext();
            return (_context.Contacts?.Any(e => e.IdContact == id)).GetValueOrDefault();
        }

        [HttpGet]
        [Route("Municipios")]
        public ContactInfo addressesCreate()
        {
            ContactInfo contactInfo = new ContactInfo();
            List<Municipio> municipios = new List<Municipio>();
            using (var context = new StoreContext())
            {
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
                    Selected = false
                };
            });

            return contactInfo;
        }

        [HttpGet]
        [Route("MunicipiosEdit")]
        public ContactInfo addressesEdit(int id)
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
                    Selected =(d.IdMunicipio==contactInfo.Contact.IdMunicipio),

                };

            });

            //foreach (var item in contactInfo.municipios)
            //{
            //    if (item.Value == contactInfo.Contact.IdMunicipio.ToString())
            //    {
            //        item.Selected = true;
            //    }
            //    else
            //    {
            //        item.Selected = false;
            //    }
            //}


            return contactInfo;
        }
    }
}
