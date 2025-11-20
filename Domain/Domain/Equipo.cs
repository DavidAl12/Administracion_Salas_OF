using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Equipo
    {
        public int Id { get; set; }

        [Required]
        public string Serial { get; set; }

        [Required]
        public string Especificaciones { get; set; }

        public string Estado { get; set; } = "Disponible";

        [Required]
        public int SalaId { get; set; }

        [ForeignKey("SalaId")]
        public Sala Sala { get; set; }
    }
}
