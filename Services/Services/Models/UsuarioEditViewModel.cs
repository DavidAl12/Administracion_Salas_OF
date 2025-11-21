using System.ComponentModel.DataAnnotations;

namespace Servicios.Models
{
    public class UsuarioEditViewModel
    {
        public string Id { get; set; } // AHORA ES STRING

        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Rol { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
