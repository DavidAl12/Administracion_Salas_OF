using System;

namespace Domain
{
    public class Reporte
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int? EquipoId { get; set; }
        public Equipo? Equipo { get; set; }

        public int? SalaId { get; set; }
        public Sala? Sala { get; set; }
    }
}
