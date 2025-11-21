using System;

namespace Servicios.Models
{
    public class CoordinadorSolicitudListViewModel
    {
        public string Tipo { get; set; } // "Sala" o "Equipo"
        public string Recurso { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; }
        public int SolicitudId { get; set; }
    }
}

