using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcSample.Controllers
{
    public class EquipoController : Controller
    {
        private readonly IEquipoService _equipoService;
        private readonly ISalaService _salaService;

        public EquipoController(IEquipoService equipoService, ISalaService salaService)
        {
            _equipoService = equipoService;
            _salaService = salaService;
        }

        public async Task<IActionResult> Index()
        {
            var equipos = await _equipoService.GetAllAsync();
            return View(equipos);
        }

        public async Task<IActionResult> Create()
        {
            var salas = await _salaService.GetAllAsync();
            ViewBag.Salas = new SelectList(salas, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                var salas = await _salaService.GetAllAsync();
                ViewBag.Salas = new SelectList(salas, "Id", "Nombre");
                return View(equipo);
            }

            await _equipoService.AddAsync(equipo);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var equipo = await _equipoService.GetByIdAsync(id);
            if (equipo == null) return NotFound();

            var salas = await _salaService.GetAllAsync();
            ViewBag.Salas = new SelectList(salas, "Id", "Nombre", equipo.SalaId);
            return View(equipo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                var salas = await _salaService.GetAllAsync();
                ViewBag.Salas = new SelectList(salas, "Id", "Nombre", equipo.SalaId);
                return View(equipo);
            }

            await _equipoService.UpdateAsync(equipo);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var equipo = await _equipoService.GetByIdAsync(id);
            if (equipo == null) return NotFound();
            return View(equipo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _equipoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
