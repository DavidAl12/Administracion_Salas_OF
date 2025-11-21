using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MvcSample.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var totalSalas = await _context.Salas.CountAsync();
            var salasActivas = await _context.Salas.Where(x => x.Estado == "Disponible").CountAsync();
            var totalEquipos = await _context.Equipos.CountAsync();
            var equiposDisponibles = await _context.Equipos.Where(x => x.Estado == "Disponible").CountAsync();
            var totalUsuarios = await _context.Users.CountAsync();

            ViewBag.SalasActivas = salasActivas;
            ViewBag.TotalSalas = totalSalas;

            ViewBag.EquiposDisponibles = equiposDisponibles;
            ViewBag.TotalEquipos = totalEquipos;

            ViewBag.TotalUsuarios = totalUsuarios;

            return View();
        }
    }
}
