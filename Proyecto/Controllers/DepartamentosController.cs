using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ProyectoModels.Models;

namespace Proyecto.Controllers
{
    public class DepartamentosController : Controller
    {

        string url = ApiUrl.url;
        // GET: Departamentoes
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var departamentos = await client.GetFromJsonAsync<IEnumerable<Departamento>>(url + "api/Departamentos");

            return View(departamentos);
        }


        // GET: Departamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDepartamento,Name")] Departamento departamento)
        {
            HttpClient client = new HttpClient();
            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync(url + "api/Departamentos", departamento);
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            HttpClient client = new HttpClient();

            var departamento = await client.GetFromJsonAsync<Departamento>(url + "api/Departamentos/" + id.ToString()); 
            
            return View(departamento);
        }

        // POST: Departamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDepartamento,Name")] Departamento departamento)
        {

            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var response = await client.PutAsJsonAsync(url + "api/Departamentos/" + departamento.IdDepartamento, departamento);
                return RedirectToAction(nameof(Index));

            }

            return View(departamento);
        }

        // GET: Departamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            HttpClient client = new HttpClient();

            var departamento = await client.GetFromJsonAsync<Departamento>(url + "api/Departamentos/" + id.ToString());

            return View(departamento);
        }

        // POST: Departamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = new HttpClient();

            var response = await client.DeleteFromJsonAsync<Departamento>(url + "api/Departamentos/" + id.ToString());
            return RedirectToAction(nameof(Index));
        }

       
    }
}
