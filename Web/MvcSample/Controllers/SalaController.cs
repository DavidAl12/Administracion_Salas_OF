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

        public async Task<IActionResult> Index()
        {
            var salas = await _salaService.GetAllAsync();
            return View(salas);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sala sala)
        {
            if (!ModelState.IsValid)
                return View(sala);

            if (string.IsNullOrEmpty(sala.Responsable))
                sala.Responsable = "-----";

            await _salaService.AddAsync(sala);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sala = await _salaService.GetByIdAsync(id);
            if (sala == null)
                return NotFound();

            return View(sala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sala sala)
        {
            if (!ModelState.IsValid)
                return View(sala);

            if (string.IsNullOrEmpty(sala.Responsable))
                sala.Responsable = "-----";

            await _salaService.UpdateAsync(sala);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sala = await _salaService.GetByIdAsync(id);
            if (sala == null)
                return NotFound();

            return View(sala);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _salaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
