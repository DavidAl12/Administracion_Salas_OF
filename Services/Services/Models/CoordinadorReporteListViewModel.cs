using System;

namespace Servicios.Models
{
    public class CoordinadorReporteListViewModel
    {
        public string Tipo { get; set; } // "equipo" o "sala"
        public string Relacionado { get; set; } // Serial o SalaNombre
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public int ReporteId { get; set; }
    }
}
