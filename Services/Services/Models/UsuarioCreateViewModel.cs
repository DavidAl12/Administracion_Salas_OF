using System.ComponentModel.DataAnnotations;

namespace Servicios.Models
{
    public class UsuarioCreateViewModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Rol { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
