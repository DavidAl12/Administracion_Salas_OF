namespace Servicios.Models
{
    public class UsuarioDashboardViewModel
    {
        public int ReservasActivas { get; set; }
        public int TotalReservas { get; set; }
        public int ReportesPendientes { get; set; }
        public int TotalReportes { get; set; }
        public int AsesoriasPendientes { get; set; }
        public int TotalAsesorias { get; set; }
        public int EquiposDisponibles { get; set; }
        public int EquiposTotales { get; set; }
        // Si quieres mostrar salas disponibles:
        public int SalasDisponibles { get; set; }
        public int SalasTotales { get; set; }
    }
}
