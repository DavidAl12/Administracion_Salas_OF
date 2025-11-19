using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

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
            var salas = await _salaService.ObtenerTodasAsync();
            return View(salas);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sala sala)
        {
            // Limpiar propiedades que vienen nulas para que no dañen la validación
            sala.Responsable = null;
            sala.Equipos = null;
            sala.PrestamosSala = null;
            sala.Reportes = null;

            // Si el modelo NO es válido, mostrar errores en la vista
            if (!ModelState.IsValid)
            {
                return View(sala);
            }

            // Guardar en base de datos
            await _salaService.CrearAsync(sala);

            TempData["success"] = "Sala creada correctamente";
            return RedirectToAction(nameof(Index));
        }

    }
}
