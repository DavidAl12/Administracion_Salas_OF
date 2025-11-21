using System.ComponentModel.DataAnnotations;

namespace Servicios.Models
{
    public class EquipoCreateViewModel
    {
        [Required(ErrorMessage = "El serial es obligatorio")]
        public string Serial { get; set; }

        [Required(ErrorMessage = "Las especificaciones son obligatorias")]
        public string Especificaciones { get; set; }

        [Required(ErrorMessage = "La sala es obligatoria")]
        public int SalaId { get; set; }
    }
}
