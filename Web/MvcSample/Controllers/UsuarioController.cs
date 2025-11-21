using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Servicios.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

[Authorize(Roles = "Usuario")]
public class UsuarioController : Controller
{
    private readonly ISalaService _salaService;
    private readonly IEquipoService _equipoService;
    private readonly IPrestamoEquipoService _prestamoEquipoService;
    private readonly IPrestamoSalaService _prestamoSalaService;
    private readonly IReporteService _reporteService;
    private readonly IAsesoriaService _asesoriaService;
    private readonly IServicioUsuarioDashboard _dashboardService;

    public UsuarioController(
        ISalaService salaService,
        IEquipoService equipoService,
        IPrestamoEquipoService prestamoEquipoService,
        IPrestamoSalaService prestamoSalaService,
        IReporteService reporteService,
        IAsesoriaService asesoriaService,
        IServicioUsuarioDashboard dashboardService)
    {
        _salaService = salaService;
        _equipoService = equipoService;
        _prestamoEquipoService = prestamoEquipoService;
        _prestamoSalaService = prestamoSalaService;
        _reporteService = reporteService;
        _asesoriaService = asesoriaService;
        _dashboardService = dashboardService;
    }

    public IActionResult Dashboard()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var viewModel = _dashboardService.GetDashboardData(userId);
        return View(viewModel);
    }

    public async Task<IActionResult> ConsultarSalas()
    {
        var salas = await _salaService.GetAllAsync();
        var viewModel = salas.Select(s => new SalaListViewModel
        {
            Id = s.Id,
            Nombre = s.Nombre,
            Ubicacion = s.Ubicacion,
            Capacidad = s.Capacidad,
            Estado = s.Estado,
            TotalEquipos = s.Equipos?.Count ?? 0,
            EquiposDisponibles = s.Equipos?.Count(e => e.Estado == "Disponible") ?? 0
        }).ToList();
        return View(viewModel);
    }

    public IActionResult SolicitarRecursos()
    {
        return View();
    }

    public async Task<IActionResult> SolicitarSala()
    {
        var salas = (await _salaService.GetAllAsync())
            .Select(s => (s.Id, s.Nombre)).ToList();

        var model = new SolicitarSalaViewModel
        {
            SalasDisponibles = salas
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SolicitarSala(SolicitarSalaViewModel model)
    {
        model.SalasDisponibles = (await _salaService.GetAllAsync())
            .Select(s => (s.Id, s.Nombre)).ToList();

        if (!ModelState.IsValid)
            return View(model);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var solicitud = new Domain.PrestamoSala
        {
            SalaId = model.SalaId,
            UsuarioId = userId,
            FechaInicio = model.FechaInicio,
            FechaFin = model.FechaFin,
            Estado = "Pendiente",
            AprobadoPor = null
        };
        await _prestamoSalaService.AddAsync(solicitud);

        TempData["Success"] = "Solicitud de sala enviada correctamente";
        return RedirectToAction("Dashboard");
    }

    public async Task<IActionResult> SolicitarEquipo()
    {
        var salas = (await _salaService.GetAllAsync()).Select(s => (s.Id, s.Nombre)).ToList();

        var model = new SolicitarEquipoViewModel
        {
            SalasDisponibles = salas,
            EquiposDisponibles = new List<SolicitarEquipoViewModel.EquipoItem>()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SolicitarEquipo(SolicitarEquipoViewModel model)
    {
        model.SalasDisponibles = (await _salaService.GetAllAsync()).Select(s => (s.Id, s.Nombre)).ToList();

        if (model.SalaId > 0)
        {
            model.EquiposDisponibles = (await _equipoService.GetAllAsync())
                .Where(e => e.SalaId == model.SalaId && e.Estado == "Disponible")
                .Select(e => new SolicitarEquipoViewModel.EquipoItem { Id = e.Id, Serial = e.Serial })
                .ToList();
        }
        else
        {
            model.EquiposDisponibles = new List<SolicitarEquipoViewModel.EquipoItem>();
        }

        if (!ModelState.IsValid)
            return View(model);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var solicitud = new Domain.PrestamoEquipo
        {
            EquipoId = model.EquipoId,
            UsuarioId = userId,
            FechaInicio = model.FechaInicio,
            FechaFin = model.FechaFin,
            Estado = "Pendiente",
            AprobadoPor = null
        };
        await _prestamoEquipoService.AddAsync(solicitud);

        TempData["Success"] = "Solicitud de equipo enviada correctamente";
        return RedirectToAction("Dashboard");
    }

    public async Task<JsonResult> EquiposPorSala(int salaId)
    {
        var equipos = (await _equipoService.GetAllAsync())
            .Where(e => e.SalaId == salaId && e.Estado == "Disponible")
            .Select(e => new { id = e.Id, serial = e.Serial })
            .ToList();
        return Json(equipos);
    }

    public async Task<IActionResult> ReportarDaño()
    {
        var model = new ReporteViewModel
        {
            ListaEquipos = (await _equipoService.GetAllAsync())
                .Select(e => new ReporteViewModel.EquipoSelectItem { Id = e.Id, Nombre = e.Serial }).ToList(),
            ListaSalas = (await _salaService.GetAllAsync())
                .Select(s => new ReporteViewModel.SalaSelectItem { Id = s.Id, Nombre = s.Nombre }).ToList()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReportarDaño(ReporteViewModel model)
    {
        model.ListaEquipos = (await _equipoService.GetAllAsync())
            .Select(e => new ReporteViewModel.EquipoSelectItem { Id = e.Id, Nombre = e.Serial }).ToList();
        model.ListaSalas = (await _salaService.GetAllAsync())
            .Select(s => new ReporteViewModel.SalaSelectItem { Id = s.Id, Nombre = s.Nombre }).ToList();

        if (!ModelState.IsValid)
            return View(model);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var reporte = new Domain.Reporte
        {
            Tipo = model.TipoReporte,
            Descripcion = model.Descripcion,
            Estado = "Pendiente",
            Fecha = DateTime.Now,
            UsuarioId = userId,
            EquipoId = model.TipoReporte == "Equipo" ? model.EquipoId : null,
            SalaId = model.TipoReporte == "Sala" ? model.SalaId : null
        };

        await _reporteService.AddAsync(reporte);

        TempData["Success"] = "Reporte enviado correctamente y será revisado por el coordinador.";
        return RedirectToAction("Dashboard");
    }

    public IActionResult SolicitarAsesoria()
    {
        var model = new AsesoriaViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SolicitarAsesoria(AsesoriaViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        TempData["Success"] = "Asesoría solicitada correctamente";
        return RedirectToAction("Dashboard");
    }

    public async Task<IActionResult> MisReservas()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var prestamosEquipo = (await _prestamoEquipoService.GetAllAsync())
            .Where(p => p.UsuarioId == userId).ToList();
        var prestamosSala = (await _prestamoSalaService.GetAllAsync())
            .Where(p => p.UsuarioId == userId).ToList();

        var equipos = (await _equipoService.GetAllAsync()).ToList();
        var salas = (await _salaService.GetAllAsync()).ToList();

        var historialEquipos = prestamosEquipo.Select(p =>
        {
            var equipo = equipos.FirstOrDefault(e => e.Id == p.EquipoId);
            var salaEquipo = equipo != null && equipo.SalaId.HasValue
                ? salas.FirstOrDefault(s => s.Id == equipo.SalaId.Value)
                : null;
            return new HistorialReservaViewModel
            {
                Tipo = "Equipo",
                Recurso = equipo?.Serial ?? "N/D",
                Sala = salaEquipo?.Nombre ?? "N/D",
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                Estado = p.Estado,
                IdReserva = p.Id,
                PuedeLiberar = p.Estado == "Activa"
            };
        });

        var historialSalas = prestamosSala.Select(p =>
        {
            var sala = salas.FirstOrDefault(s => s.Id == p.SalaId);
            return new HistorialReservaViewModel
            {
                Tipo = "Sala",
                Recurso = sala?.Nombre ?? "N/D",
                Sala = sala?.Nombre ?? "N/D",
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                Estado = p.Estado,
                IdReserva = p.Id,
                PuedeLiberar = p.Estado == "Activa"
            };
        });

        var historial = historialEquipos
            .Concat(historialSalas)
            .OrderByDescending(r => r.Estado)
            .ThenByDescending(r => r.FechaInicio)
            .ToList();

        return View(historial);
    }
}
