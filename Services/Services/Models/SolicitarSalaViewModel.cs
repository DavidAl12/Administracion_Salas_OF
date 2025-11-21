using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Servicios.Models
{
    public class SolicitarSalaViewModel
    {
        [Required(ErrorMessage = "Debe seleccionar una sala")]
        public int SalaId { get; set; }

        public List<(int Id, string Nombre)> SalasDisponibles { get; set; } = new List<(int, string)>();

        [Required(ErrorMessage = "La fecha y hora de inicio son obligatorias")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha y hora de fin son obligatorias")]
        public DateTime FechaFin { get; set; }
    }
}
