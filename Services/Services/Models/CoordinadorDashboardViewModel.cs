namespace Servicios.Models
{
    public class CoordinadorDashboardViewModel
    {
        // Resumen principal
        public int EquiposDisponibles { get; set; }
        public int EquiposTotales { get; set; }
        public int SolicitudesPendientes { get; set; }
        public int ReportesPendientes { get; set; }

        // Puedes expandir según la información para las tarjetas del dashboard
    }
}
