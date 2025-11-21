using Microsoft.AspNetCore.Mvc;
using Services;
using Servicios.Models;
using Domain;
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
        public async Task<IActionResult> Create(SalaCreateViewModel input)
        {
            if (!ModelState.IsValid)
                return View(input);

            var sala = new Sala
            {
                Nombre = input.Nombre,
                Ubicacion = input.Ubicacion,
                Capacidad = input.Capacidad,
                Estado = input.Estado,
                Responsable = string.IsNullOrEmpty(input.Responsable) ? "-----" : input.Responsable
            };

            await _salaService.AddAsync(sala);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sala = await _salaService.GetByIdAsync(id);
            if (sala == null)
                return NotFound();

            var vm = new SalaEditViewModel
            {
                Id = sala.Id,
                Nombre = sala.Nombre,
                Ubicacion = sala.Ubicacion,
                Capacidad = sala.Capacidad,
                Estado = sala.Estado,
                Responsable = sala.Responsable
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SalaEditViewModel input)
        {
            if (!ModelState.IsValid)
                return View(input);

            var sala = await _salaService.GetByIdAsync(input.Id);
            if (sala == null)
                return NotFound();

            sala.Nombre = input.Nombre;
            sala.Ubicacion = input.Ubicacion;
            sala.Capacidad = input.Capacidad;
            sala.Estado = input.Estado;
            sala.Responsable = string.IsNullOrEmpty(input.Responsable) ? "-----" : input.Responsable;

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
