using Domain;
using Infrastructure;
using Servicios.Models;
using System.Linq;

public class ServicioUsuarioDashboard : IServicioUsuarioDashboard
{
    private readonly AppDbContext _dbContext;

    public ServicioUsuarioDashboard(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public UsuarioDashboardViewModel GetDashboardData(string usuarioId)
    {
        var reservasActivas = _dbContext.PrestamosSala.Count(r => r.UsuarioId == usuarioId && r.Estado == "Activa");
        var totalReservas = _dbContext.PrestamosSala.Count(r => r.UsuarioId == usuarioId);

        var reportesPendientes = _dbContext.Reportes.Count(r => r.UsuarioId == usuarioId && r.Estado == "Pendiente");
        var totalReportes = _dbContext.Reportes.Count(r => r.UsuarioId == usuarioId);

        var asesoriasPendientes = _dbContext.Asesorias.Count(a => a.UsuarioId == usuarioId && a.Estado == "Pendiente");
        var totalAsesorias = _dbContext.Asesorias.Count(a => a.UsuarioId == usuarioId);

        var equiposTotales = _dbContext.Equipos.Count();
        var equiposDisponibles = _dbContext.Equipos.Count(e => e.Estado == "Disponible");

        var salasTotales = _dbContext.Salas.Count();
        var salasDisponibles = _dbContext.Salas.Count(s => s.Estado == "Disponible");

        return new UsuarioDashboardViewModel
        {
            ReservasActivas = reservasActivas,
            TotalReservas = totalReservas,
            ReportesPendientes = reportesPendientes,
            TotalReportes = totalReportes,
            AsesoriasPendientes = asesoriasPendientes,
            TotalAsesorias = totalAsesorias,
            EquiposTotales = equiposTotales,
            EquiposDisponibles = equiposDisponibles,
            SalasTotales = salasTotales,
            SalasDisponibles = salasDisponibles
        };
    }
}
