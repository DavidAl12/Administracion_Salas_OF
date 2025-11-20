using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Usuario : IdentityUser
    {
        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public string Rol { get; set; }

        public ICollection<PrestamoEquipo> PrestamosEquipo { get; set; }
        public ICollection<PrestamoSala> PrestamosSala { get; set; }
        public ICollection<Reporte> Reportes { get; set; }
        public ICollection<Asesoria> Asesorias { get; set; }
    }
}
