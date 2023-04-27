using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using ProyectoModels.Models;

namespace Proyecto.Controllers
{
    public class MunicipiosController : Controller
    {
        private readonly StoreContext _context = new StoreContext();

        public string url = ApiUrl.url;
        

        // GET: Municipios
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var municipios =await client.GetFromJsonAsync<IEnumerable<Municipio>>(url + "api/Municipios");
            return View(municipios);
        }


        // GET: Municipios/Create
        public async Task<IActionResult> Create()
        {
            HttpClient client = new HttpClient();
            var departamentos = await client.GetFromJsonAsync<List<SelectListItem>>(url + "api/Municipios/Departamentos");
            ViewData["IdDepartamento"] = departamentos;
            return View();
        }

        // POST: Municipios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Municipio municipio)
        {
            HttpClient client = new HttpClient();

            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync<Municipio>(url + "api/Municipios", municipio);
            }
            else
            {
                ViewData["IdDepartamento"] = await client.GetFromJsonAsync<List<SelectListItem>>(url + "api/Municipios/Departamentos");
                return View(municipio);
            }
            

            return RedirectToAction(nameof(Index));
        }

        // GET: Municipios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = new HttpClient();
            var municipio = await client.GetFromJsonAsync<Municipio>(url + "api/Municipios/" + id.ToString());
            ViewData["IdDepartamento"] = await client.GetFromJsonAsync<List<SelectListItem>>(url + "api/Municipios/Departamentos");
            return View(municipio);
        }

        // POST: Municipios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMunicipio,Name,IdDepartamento")] Municipio municipio)
        {
            HttpClient client = new HttpClient();
            if (id != municipio.IdMunicipio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                var response = await client.PutAsJsonAsync(url+"api/Municipios/" +municipio.IdMunicipio.ToString(), municipio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = await client.GetFromJsonAsync<List<SelectListItem>>(url + "api/Municipios/Departamentos");
            return View(municipio);
        }

        // GET: Municipios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            HttpClient client = new HttpClient();
            if (id == null || _context.Municipios == null)
            {
                return NotFound();
            }

            var municipio = await client.GetFromJsonAsync<Municipio>(url + "api/Municipios/" + id.ToString());
            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = new HttpClient();
            if (_context.Municipios == null)
            {
                return Problem("Entity set 'StoreContext.Municipios'  is null.");
            }

            var response = await client.DeleteFromJsonAsync<Municipio>(url + "api/Municipios/" + id.ToString());

            return RedirectToAction(nameof(Index));
        }

        private bool MunicipioExists(int id)
        {
          return (_context.Municipios?.Any(e => e.IdMunicipio == id)).GetValueOrDefault();
        }
    }
}
