using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.Threading.Tasks;

namespace MvcSample.Controllers
{
    public class EquipoController : Controller
    {
        private readonly AppDbContext _context;

        public EquipoController(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var equipos = await _context.Equipos.Include(x => x.Sala).ToListAsync();
            return View(equipos);
        }

        // CREAR GET
        public async Task<IActionResult> Create()
        {
            ViewBag.Salas = await _context.Salas.ToListAsync();
            return View();
        }

        // CREAR POST
        [HttpPost]
        public async Task<IActionResult> Create(Equipo eq)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Salas = await _context.Salas.ToListAsync();
                return View(eq);
            }

            _context.Equipos.Add(eq);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // EDITAR GET
        public async Task<IActionResult> Edit(int id)
        {
            var eq = await _context.Equipos.FindAsync(id);

            if (eq == null)
                return NotFound();

            ViewBag.Salas = await _context.Salas.ToListAsync();
            return View(eq);
        }

        // EDITAR POST
        [HttpPost]
        public async Task<IActionResult> Edit(Equipo eq)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Salas = await _context.Salas.ToListAsync();
                return View(eq);
            }

            _context.Equipos.Update(eq);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // ELIMINAR GET
        public async Task<IActionResult> Delete(int id)
        {
            var eq = await _context.Equipos.Include(x => x.Sala).FirstOrDefaultAsync(x => x.Id == id);

            if (eq == null)
                return NotFound();

            return View(eq);
        }

        // ELIMINAR POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eq = await _context.Equipos.FindAsync(id);

            if (eq == null)
                return NotFound();

            _context.Equipos.Remove(eq);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
