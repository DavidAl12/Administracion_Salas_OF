using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; }

        // Identity se encargará de Email & Password

        [Required]
        public string Rol { get; set; } // Normal | Coordinador | Administrador

        public ICollection<PrestamoEquipo>? PrestamosEquipo { get; set; } = new List<PrestamoEquipo>();
        public ICollection<PrestamoSala>? PrestamosSala { get; set; } = new List<PrestamoSala>();
        public ICollection<Reporte>? Reportes { get; set; } = new List<Reporte>();
        public ICollection<Asesoria>? Asesorias { get; set; } = new List<Asesoria>();
    }
}
