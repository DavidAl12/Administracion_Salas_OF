using System.Collections.Generic;

namespace Servicios.Models
{
    public class CoordinadorSalaOcupacionViewModel
    {
        public string SalaNombre { get; set; }
        public string Ubicacion { get; set; }
        public int TotalEquipos { get; set; }
        public int EquiposDisponibles { get; set; }
        public int EquiposOcupados { get; set; }
        public int PorcentajeOcupacion { get; set; }
    }
}
