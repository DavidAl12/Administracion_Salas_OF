using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Sala
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Ubicacion { get; set; }

        [StringLength(50)]
        public string Estado { get; set; } // Ejemplo: "Libre", "Ocupada", "Mantenimiento"

        public int Capacidad { get; set; }

        // Campo opcional con valor por defecto
        public string? Responsable { get; set; } = "-----";

        // Navegación - Colección de equipos
        public ICollection<Equipo>? Equipos { get; set; } = new List<Equipo>();

        public ICollection<PrestamoSala>? PrestamosSala { get; set; } = new List<PrestamoSala>();

        public ICollection<Reporte>? Reportes { get; set; } = new List<Reporte>();
    }
}
