using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Servicios.Models;
using Services;
using Domain; // Para el modelo Usuario
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class CoordinadorController : Controller
{
    private readonly IEquipoService _equipoService;
    private readonly ISalaService _salaService;
    private readonly IPrestamoEquipoService _prestamoEquipoService;
    private readonly IPrestamoSalaService _prestamoSalaService;
    private readonly IReporteService _reporteService;
    private readonly IAsesoriaService _asesoriaService;
    private readonly UserManager<Usuario> _userManager;

    public CoordinadorController(
        IEquipoService equipoService,
        ISalaService salaService,
        IPrestamoEquipoService prestamoEquipoService,
        IPrestamoSalaService prestamoSalaService,
        IReporteService reporteService,
        IAsesoriaService asesoriaService,
        UserManager<Usuario> userManager
    )
    {
        _equipoService = equipoService;
        _salaService = salaService;
        _prestamoEquipoService = prestamoEquipoService;
        _prestamoSalaService = prestamoSalaService;
        _reporteService = reporteService;
        _asesoriaService = asesoriaService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Dashboard()
    {
        var equipos = await _equipoService.GetAllAsync();
        var solicitudesPendientes =
            (await _prestamoEquipoService.GetAllAsync()).Count(p => p.Estado == "Pendiente") +
            (await _prestamoSalaService.GetAllAsync()).Count(p => p.Estado == "Pendiente");
        var reportesPendientes = (await _reporteService.GetAllAsync()).Count(r => r.Estado == "Pendiente");

        var viewModel = new CoordinadorDashboardViewModel
        {
            EquiposDisponibles = equipos.Count(e => e.Estado == "Disponible"),
            EquiposTotales = equipos.Count(),
            SolicitudesPendientes = solicitudesPendientes,
            ReportesPendientes = reportesPendientes
        };
        return View(viewModel);
    }

    public async Task<IActionResult> Equipos()
    {
        var equipos = await _equipoService.GetAllAsync();
        var model = equipos.Select(e => new CoordinadorEquipoListViewModel
        {
            Id = e.Id,
            Serial = e.Serial,
            SalaNombre = e.Sala?.Nombre ?? "Sin sala",
            Especificaciones = e.Especificaciones,
            Estado = e.Estado
        }).ToList();
        return View(model);
    }

    public async Task<IActionResult> Solicitudes()
    {
        var prestamosEquipo = (await _prestamoEquipoService.GetAllAsync()).ToList();
        var prestamosSala = (await _prestamoSalaService.GetAllAsync()).ToList();
        var equipos = (await _equipoService.GetAllAsync()).ToList();
        var salas = (await _salaService.GetAllAsync()).ToList();

        // Junta todos los Ids únicos de usuario involucrados en solicitudes
        var allUserIds = prestamosEquipo.Select(p => p.UsuarioId)
            .Concat(prestamosSala.Select(p => p.UsuarioId))
            .Where(id => !string.IsNullOrEmpty(id))
            .Distinct()
            .ToList();

        // Descarga los usuarios de Identity y asocia su Nombre
        var userDict = new Dictionary<string, string>();
        foreach (var userId in allUserIds)
        {
            var user = await _userManager.FindByIdAsync(userId);
            userDict[userId] = user?.Nombre ?? user?.UserName ?? userId ?? "N/D";
        }

        var solicitudes = prestamosEquipo.Select(p =>
        {
            var equipo = equipos.FirstOrDefault(e => e.Id == p.EquipoId);
            var nombreUsuario = userDict.TryGetValue(p.UsuarioId, out var nombre) ? nombre : p.UsuarioId ?? "N/D";

            return new CoordinadorSolicitudListViewModel
            {
                Tipo = "Equipo",
                Recurso = equipo?.Serial ?? "N/D",
                Usuario = nombreUsuario,
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                Estado = p.Estado,
                SolicitudId = p.Id
            };
        }).ToList();

        solicitudes.AddRange(prestamosSala.Select(p =>
        {
            var sala = salas.FirstOrDefault(s => s.Id == p.SalaId);
            var nombreUsuario = userDict.TryGetValue(p.UsuarioId, out var nombre) ? nombre : p.UsuarioId ?? "N/D";

            return new CoordinadorSolicitudListViewModel
            {
                Tipo = "Sala",
                Recurso = sala?.Nombre ?? "N/D",
                Usuario = nombreUsuario,
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                Estado = p.Estado,
                SolicitudId = p.Id
            };
        }));

        return View(solicitudes
            .OrderByDescending(s => s.Estado)
            .ThenByDescending(s => s.FechaInicio)
            .ToList());
    }

    public async Task<IActionResult> Ocupacion()
    {
        var salas = await _salaService.GetAllAsync();
        var model = salas.Select(s =>
        {
            int total = s.Equipos?.Count ?? 0;
            int disponibles = s.Equipos?.Count(e => e.Estado == "Disponible") ?? 0;
            int ocupados = total - disponibles;
            int porcentaje = total > 0 ? (int)Math.Round(100.0 * ocupados / total) : 0;

            return new CoordinadorSalaOcupacionViewModel
            {
                SalaNombre = s.Nombre,
                Ubicacion = s.Ubicacion,
                TotalEquipos = total,
                EquiposDisponibles = disponibles,
                EquiposOcupados = ocupados,
                PorcentajeOcupacion = porcentaje
            };
        }).ToList();

        return View(model);
    }

    public async Task<IActionResult> Reportes()
    {
        var reportes = await _reporteService.GetAllAsync();

        // Todos los UsuarioId únicos en reportes
        var allUserIds = reportes.Select(r => r.UsuarioId)
            .Where(id => !string.IsNullOrEmpty(id))
            .Distinct().ToList();

        var userDict = new Dictionary<string, string>();
        foreach (var userId in allUserIds)
        {
            var user = await _userManager.FindByIdAsync(userId);
            userDict[userId] = user?.Nombre ?? user?.UserName ?? userId ?? "";
        }

        var model = reportes.Select(r =>
        {
            var nombreUsuario = userDict.ContainsKey(r.UsuarioId) ? userDict[r.UsuarioId] : r.UsuarioId ?? "";
            return new CoordinadorReporteListViewModel
            {
                Tipo = r.EquipoId.HasValue ? "equipo" : "sala",
                Relacionado = r.EquipoId.HasValue ? (r.Equipo?.Serial ?? "") : (r.Sala?.Nombre ?? ""),
                Descripcion = r.Descripcion,
                Estado = r.Estado,
                Fecha = r.Fecha,
                Usuario = nombreUsuario,
                ReporteId = r.Id
            };
        }).ToList();
        return View(model);
    }
    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> CambiarEstado(int id, string tipo, string estado)
    {
        bool actualizado = false;

        if (tipo == "Equipo")
        {
            var solicitud = await _prestamoEquipoService.GetByIdAsync(id);
            if (solicitud != null)
            {
                solicitud.Estado = estado;
                await _prestamoEquipoService.UpdateAsync(solicitud);
                actualizado = true;
            }
        }
        else if (tipo == "Sala")
        {
            var solicitud = await _prestamoSalaService.GetByIdAsync(id);
            if (solicitud != null)
            {
                solicitud.Estado = estado;
                await _prestamoSalaService.UpdateAsync(solicitud);
                actualizado = true;
            }
        }

        return Json(new { success = actualizado, nuevoEstado = estado });
    }


    public IActionResult Informes()
    {
        return View();
    }
}
