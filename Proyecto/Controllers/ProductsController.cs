using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;
using System.Web;
using ProyectoModels.ViewModels;

namespace Proyecto.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StoreContext _context = new StoreContext();

        public static IWebHostEnvironment rutasDeSubida;

        public ProductsController(IWebHostEnvironment _rutas)
        {
            rutasDeSubida = _rutas;
        }

        string url = ApiUrl.url;

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var products = await client.GetFromJsonAsync<IEnumerable<Product>>(url + "api/Products");
              return products != null ? 
                          View(products) :
                          Problem("Entity set 'StoreContext.Products'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,Name,Description,Price,Stock,Photo")] Product product, IFormFile foto)
        {
            HttpClient client = new HttpClient();
            if (ModelState.IsValid)
            {
                if (!Directory.Exists(rutasDeSubida.WebRootPath + "\\Fotos\\"))
                {
                Directory.CreateDirectory(rutasDeSubida.WebRootPath + "\\Fotos\\");
                }
                using (FileStream stream = System.IO.File.Create(rutasDeSubida.WebRootPath + "\\Fotos\\" + foto.FileName))
                {
                foto.CopyTo(stream);
                stream.Flush();
                }

                product.Photo =  "/Fotos/" + foto.FileName;

                var response = await client.PostAsJsonAsync(url + "api/Products", product);
            
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();
            var product = await client.GetFromJsonAsync<Product>(url + "api/Products/" + id.ToString());

            if (product == null)
            {
                return NotFound();
            }
            

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduct,Name,Description,Price,Stock,Photo")] Product product, IFormFile? foto)
        {
            HttpClient client = new HttpClient();
            if (id != product.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (foto != null)
                    {
                        using (FileStream stream = System.IO.File.Create(rutasDeSubida.WebRootPath + "\\Fotos\\" + foto.FileName))
                        {
                            foto.CopyTo(stream);
                            stream.Flush();
                        }
                        product.Photo = "/Fotos/" + foto.FileName;
                    }

                    var response = await client.PutAsJsonAsync(url + "api/Products/" + product.IdProduct.ToString(), product);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            HttpClient client = new HttpClient();
            if (id == null )
            {
                return NotFound();
            }

            var product = await client.GetFromJsonAsync<Product>(url + "api/Products/" + id.ToString());
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'StoreContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.IdProduct == id)).GetValueOrDefault();
        }
    }
}
