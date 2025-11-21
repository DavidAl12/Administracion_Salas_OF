using Servicios.Models;

public interface IServicioUsuarioDashboard
{
    UsuarioDashboardViewModel GetDashboardData(string usuarioId);
}