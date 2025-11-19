using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Equipo
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Serial { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        public int? SalaId { get; set; }
        public Sala Sala { get; set; }

        public ICollection<PrestamoEquipo> PrestamosEquipo { get; set; }
        public ICollection<Reporte> Reportes { get; set; }
    }
}
