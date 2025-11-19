using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks;

namespace MvcSample.Controllers
{
    public class SalaController : Controller
    {
        private readonly ISalaService _salaService;

        public SalaController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        // GET: Sala
        public async Task<IActionResult> Index()
        {
            var salas = await _salaService.ObtenerTodasAsync();
            return View(salas);
        }

        // GET: Sala/Create
        public IActionResult Create() => View();

        // POST: Sala/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sala sala)
        {
            sala.Responsable = null;
            sala.Equipos = null;
            sala.PrestamosSala = null;
            sala.Reportes = null;

            if (!ModelState.IsValid)
            {
                return View(sala);
            }

            await _salaService.CrearAsync(sala);
            TempData["success"] = "Sala creada correctamente";
            return RedirectToAction(nameof(Index));
        }

        // GET: Sala/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sala = await _salaService.ObtenerPorIdAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            return View(sala);
        }

        // POST: Sala/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sala sala)
        {
            if (!ModelState.IsValid)
            {
                return View(sala);
            }

            await _salaService.ActualizarAsync(sala);
            TempData["success"] = "Sala actualizada correctamente";
            return RedirectToAction(nameof(Index));
        }

        // GET: Sala/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var sala = await _salaService.ObtenerPorIdAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            return View(sala);
        }

        // POST: Sala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _salaService.EliminarAsync(id);
            TempData["success"] = "Sala eliminada correctamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
