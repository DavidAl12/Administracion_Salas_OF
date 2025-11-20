using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Equipo
    {
        public int Id { get; set; }

        public string Serial { get; set; }

        public string Especificaciones { get; set; }

        public string Estado { get; set; } = "Disponible";

        public int SalaId { get; set; }
        public Sala Sala { get; set; }
    }

}
