using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;
using ProyectoModels.ViewModels;


namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        StoreContext _context = new StoreContext();

        [HttpGet]
        public async Task<Kpis> Kpis()
        {
            Kpis kpis = new Kpis();
            kpis.PorConfirmar=await _context.OrderHeaders.Where(p => p.OrderStatus == "Por confirmar").CountAsync();
            kpis.Confirmadas= await _context.OrderHeaders.Where(p => p.OrderStatus == "Confirmada").CountAsync();
            kpis.Enviadas = await _context.OrderHeaders.Where(p => p.OrderStatus == "Enviada").CountAsync();
            kpis.Entregadas = await _context.OrderHeaders.Where(p => p.OrderStatus == "Entregada").CountAsync();

            return kpis;

        }
    }
}
