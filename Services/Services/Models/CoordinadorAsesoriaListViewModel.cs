using System;

namespace Servicios.Models
{
    public class CoordinadorAsesoriaListViewModel
    {
        public int Id { get; set; }
        public DateTime FechaPreferida { get; set; } // Para la vista
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
    }
}
