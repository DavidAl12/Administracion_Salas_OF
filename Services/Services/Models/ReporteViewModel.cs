using System.Collections.Generic;

namespace Servicios.Models
{
    public class ReporteViewModel
    {
        public string TipoReporte { get; set; }
        public int? EquipoId { get; set; }
        public int? SalaId { get; set; }
        public string Descripcion { get; set; }

        // NO USAR [Required]
        public List<EquipoSelectItem> ListaEquipos { get; set; } = new List<EquipoSelectItem>();
        public List<SalaSelectItem> ListaSalas { get; set; } = new List<SalaSelectItem>();

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
