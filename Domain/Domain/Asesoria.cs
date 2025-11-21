using System;

namespace Domain
{
    public class Asesoria
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        // CAMBIO: Identity Framework, FK UsuarioId ahora es string
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        // CAMBIO: Identity Framework, FK CoordinadorId ahora es string
        public string? CoordinadorId { get; set; }
        public Usuario? Coordinador { get; set; }
    }
}
