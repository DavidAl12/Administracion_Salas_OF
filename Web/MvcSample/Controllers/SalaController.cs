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

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var salas = await _salaService.ObtenerTodasAsync();
            return View(salas);
        }

        // CREAR GET
        public IActionResult Create() => View();

        // CREAR POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sala sala)
        {
            if (!ModelState.IsValid)
                return View(sala);

            if (string.IsNullOrEmpty(sala.Responsable))
                sala.Responsable = "-----";

            await _salaService.CrearAsync(sala);

            return RedirectToAction(nameof(Index));
        }

        // EDITAR GET
        public async Task<IActionResult> Edit(int id)
        {
            var sala = await _salaService.ObtenerPorIdAsync(id);
            if (sala == null)
                return NotFound();

            return View(sala);
        }

        // EDITAR POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sala sala)
        {
            if (!ModelState.IsValid)
                return View(sala);

            if (string.IsNullOrEmpty(sala.Responsable))
                sala.Responsable = "-----";

            await _salaService.ActualizarAsync(sala);
            return RedirectToAction(nameof(Index));
        }

        // ELIMINAR GET
        public async Task<IActionResult> Delete(int id)
        {
            var sala = await _salaService.ObtenerPorIdAsync(id);
            if (sala == null)
                return NotFound();

            return View(sala);
        }

        // ELIMINAR POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _salaService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
