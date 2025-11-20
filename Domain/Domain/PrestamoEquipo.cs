using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PrestamoEquipo
    {
        public int Id { get; set; }

        public int EquipoId { get; set; }
        public Equipo Equipo { get; set; }

        // CAMBIO: Identity Framework, FK UsuarioId ahora es string
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        [StringLength(100)]
        public string AprobadoPor { get; set; }
    }
}
