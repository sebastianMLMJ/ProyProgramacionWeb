using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        // GET: api/Departamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()
        {
          StoreContext _context = new StoreContext();

          if (_context.Departamentos == null)
          {
              return NotFound();
          }
            return await _context.Departamentos.ToListAsync();
        }

        // GET: api/Departamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(int id)
        {
            StoreContext _context = new StoreContext();
            if (_context.Departamentos == null)
          {
              return NotFound();
          }
            var departamento = await _context.Departamentos.FindAsync(id);

            if (departamento == null)
            {
                return NotFound();
            }

            return departamento;
        }

        // PUT: api/Departamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(int id, Departamento departamento)
        {
            StoreContext _context = new StoreContext();
            if (id != departamento.IdDepartamento)
            {
                return BadRequest();
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
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

        // POST: api/Departamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
        {
            StoreContext _context = new StoreContext();
            if (_context.Departamentos == null)
          {
              return Problem("Entity set 'StoreContext.Departamentos'  is null.");
          }
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartamento", new { id = departamento.IdDepartamento }, departamento);
        }

        // DELETE: api/Departamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            StoreContext _context = new StoreContext();
            if (_context.Departamentos == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();

            return Ok(departamento);
        }

        private bool DepartamentoExists(int id)
        {
            StoreContext _context = new StoreContext();
            return (_context.Departamentos?.Any(e => e.IdDepartamento == id)).GetValueOrDefault();
        }
    }
}
