using System.ComponentModel.DataAnnotations;

namespace Servicios.Models
{
    public class SalaCreateViewModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Ubicacion { get; set; }

        [Required]
        public int Capacidad { get; set; }

        [Required]
        public string Estado { get; set; }

        public string? Responsable { get; set; }
    }
}
