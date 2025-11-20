using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Linq;
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

        // ================================
        //     ADMINISTRADOR
        // ================================
        public async Task<IActionResult> Index()
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != "Administrador")
                return RedirectToAction("Dashboard", "Usuario");

            var salas = await _salaService.ObtenerTodasAsync();
            return View(salas);
        }

        public IActionResult Create()
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != "Administrador")
                return RedirectToAction("Dashboard", "Usuario");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sala sala)
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != "Administrador")
                return RedirectToAction("Dashboard", "Usuario");

            if (!ModelState.IsValid)
                return View(sala);

            await _salaService.CrearAsync(sala);
            TempData["success"] = "Sala creada correctamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != "Administrador")
                return RedirectToAction("Dashboard", "Usuario");

            var sala = await _salaService.ObtenerPorIdAsync(id);
            if (sala == null)
                return NotFound();

            return View(sala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sala sala)
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != "Administrador")
                return RedirectToAction("Dashboard", "Usuario");

            if (!ModelState.IsValid)
                return View(sala);

            await _salaService.ActualizarAsync(sala);
            TempData["success"] = "Sala actualizada correctamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != "Administrador")
                return RedirectToAction("Dashboard", "Usuario");

            var sala = await _salaService.ObtenerPorIdAsync(id);
            if (sala == null)
                return NotFound();

            return View(sala);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != "Administrador")
                return RedirectToAction("Dashboard", "Usuario");

            await _salaService.EliminarAsync(id);
            TempData["success"] = "Sala eliminada correctamente";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        //     USUARIO NORMAL
        // ================================
        public async Task<IActionResult> Disponibles()
        {
            var salas = await _salaService.ObtenerTodasAsync();
            var disponibles = salas.Where(s => s.Estado == "Disponible");
            return View(disponibles);
        }

        public async Task<IActionResult> Solicitar(int id)
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != "Normal")
                return RedirectToAction("Dashboard", "Usuario");

            // TODO: reemplazar por el usuario real desde sesión / identity
            int usuarioId = 1; // temporal

            await _salaService.SolicitarSalaAsync(id, usuarioId);

            TempData["success"] = "Solicitud enviada correctamente";
            return RedirectToAction("Disponibles");
        }

        public async Task<IActionResult> MisSolicitudes()
        {
            // TODO: reemplazar por el usuario real desde sesión / identity
            int usuarioId = 1; // temporal

            var solicitudes = await _salaService.ObtenerSolicitudesUsuarioAsync(usuarioId);
            return View(solicitudes);
        }

        // ================================
        //     COORDINADOR
        // ================================
        public async Task<IActionResult> Solicitudes()
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != "Coordinador")
                return RedirectToAction("Dashboard", "Usuario");

            var solicitudes = await _salaService.ObtenerSolicitudesAsync();
            return View(solicitudes);
        }

        public async Task<IActionResult> Aceptar(int id)
        {
            await _salaService.CambiarEstadoSolicitud(id, "Aceptado");
            return RedirectToAction("Solicitudes");
        }

        public async Task<IActionResult> Denegar(int id)
        {
            await _salaService.CambiarEstadoSolicitud(id, "Denegado");
            return RedirectToAction("Solicitudes");
        }
    }
}
