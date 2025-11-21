using System.Collections.Generic;

namespace Servicios.Models
{
    public class ReporteViewModel
    {
        public string TipoReporte { get; set; }    // "Equipo" o "Sala"
        public int? EquipoId { get; set; }
        public int? SalaId { get; set; }
        public string Descripcion { get; set; }

        public List<EquipoSelectItem> ListaEquipos { get; set; }
        public List<SalaSelectItem> ListaSalas { get; set; }

        public class EquipoSelectItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }
        public class SalaSelectItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }
    }
}
