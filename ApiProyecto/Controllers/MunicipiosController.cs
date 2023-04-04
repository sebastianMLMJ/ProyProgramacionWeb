using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {
        

        // GET: api/Municipios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Municipio>>> GetMunicipios()
        {
          StoreContext _context = new StoreContext();
          if (_context.Municipios == null)
          {
              return NotFound();
          }
            return await _context.Municipios.Include(p => p.IdDepartamentoNavigation).ToListAsync();
        }

        // GET: api/Municipios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Municipio>> GetMunicipio(int id)
        {

          StoreContext _context = new StoreContext();

          if (_context.Municipios == null)
          {
              return NotFound();
          }
            var municipio = await _context.Municipios.Include(p => p.IdDepartamentoNavigation).Where(p => p.IdMunicipio == id).FirstAsync();

            if (municipio == null)
            {
                return NotFound();
            }

            return municipio;
        }

        // PUT: api/Municipios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMunicipio(int id, Municipio municipio)
        {
            StoreContext _context = new StoreContext();

            if (id != municipio.IdMunicipio)
            {
                return BadRequest();
            }

            _context.Entry(municipio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipioExists(id))
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

        // POST: api/Municipios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Municipio>> PostMunicipio(Municipio municipio)
        {
            StoreContext _context = new StoreContext();


            if (_context.Municipios == null)
          {
              return Problem("Entity set 'StoreContext.Municipios'  is null.");
          }
            _context.Municipios.Add(municipio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMunicipio", new { id = municipio.IdMunicipio }, municipio);
        }

        // DELETE: api/Municipios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMunicipio(int id)
        {
            StoreContext _context = new StoreContext();

            if (_context.Municipios == null)
            {
                return NotFound();
            }
            var municipio = await _context.Municipios.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }

            _context.Municipios.Remove(municipio);
            await _context.SaveChangesAsync();

            return Ok(municipio);
        }

        [HttpGet]
        [Route("Departamentos")]
        public List<SelectListItem> Departamentos()
        {
            StoreContext _context = new StoreContext();
            var lst = _context.Departamentos.ToList();
            var municipios = lst.ConvertAll(d => {

                return new SelectListItem
                {
                    Text = d.Name,
                    Value = d.IdDepartamento.ToString(),
                    Selected = false
                };
            });

            return municipios;
            //new SelectList(_context.Departamentos, "IdDepartamento", "Name");
        }
       

        private bool MunicipioExists(int id)
        {
            StoreContext _context = new StoreContext();

            return (_context.Municipios?.Any(e => e.IdMunicipio == id)).GetValueOrDefault();
        }
    }
}
