using System;

namespace Servicios.Models
{
    public class HistorialReservaViewModel
    {
        public string Tipo { get; set; }           // "Equipo" o "Sala"
        public string Recurso { get; set; }        // Serial o nombre
        public string Sala { get; set; }           // Nombre de la sala
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; }         // "Activa", "Pendiente", "Finalizada"...
        public int IdReserva { get; set; }
        public bool PuedeLiberar { get; set; }
    }
}
