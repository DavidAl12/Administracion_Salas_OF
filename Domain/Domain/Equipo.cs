using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Equipo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El serial es obligatorio")]
        public string Serial { get; set; }

        [Required(ErrorMessage = "Las especificaciones son obligatorias")]
        public string Especificaciones { get; set; }

        public string Estado { get; set; } = "Disponible";

        // ✔ Debe ser nullable para usar SET NULL
        // ❌ No debe tener [Required]
        public int? SalaId { get; set; }

        public Sala Sala { get; set; }
    }
}
