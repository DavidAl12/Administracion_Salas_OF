using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Rol { get; set; }

        public ICollection<PrestamoEquipo> PrestamosEquipo { get; set; }
        public ICollection<PrestamoSala> PrestamosSala { get; set; }
        public ICollection<Reporte> Reportes { get; set; }
        public ICollection<Asesoria> Asesorias { get; set; }
    }
}
