using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PrestamoSala
    {
        public int Id { get; set; }

        public int SalaId { get; set; }
        public Sala Sala { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public string Estado { get; set; }
        public string AprobadoPor { get; set; }
    }
}
