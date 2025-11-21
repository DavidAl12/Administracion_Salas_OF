using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using Servicios.Models;
using Domain;
using System.Threading.Tasks;

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
            ViewBag.Salas = new SelectList(await _salaService.GetAllAsync(), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipoCreateViewModel input)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Salas = new SelectList(await _salaService.GetAllAsync(), "Id", "Nombre");
                return View(input);
            }

            var equipo = new Equipo
            {
                Serial = input.Serial,
                Especificaciones = input.Especificaciones,
                SalaId = input.SalaId
            };
            await _equipoService.AddAsync(equipo);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var equipo = await _equipoService.GetByIdAsync(id);
            if (equipo == null) return NotFound();

            var viewModel = new EquipoEditViewModel
            {
                Id = equipo.Id,
                Serial = equipo.Serial,
                Especificaciones = equipo.Especificaciones,
                SalaId = equipo.SalaId ?? 0
            };

            ViewBag.Salas = new SelectList(await _salaService.GetAllAsync(), "Id", "Nombre", equipo.SalaId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EquipoEditViewModel input)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Salas = new SelectList(await _salaService.GetAllAsync(), "Id", "Nombre", input.SalaId);
                return View(input);
            }

            var equipo = await _equipoService.GetByIdAsync(input.Id);
            if (equipo == null) return NotFound();

            equipo.Serial = input.Serial;
            equipo.Especificaciones = input.Especificaciones;
            equipo.SalaId = input.SalaId;

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
