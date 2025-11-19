using System;

namespace Domain
{
    public class Asesoria
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int? CoordinadorId { get; set; }
        public Usuario Coordinador { get; set; }
    }
}
