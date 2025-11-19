using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Prestamo
    {

        [Key]
        public Guid Id { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string Estado { get; set; } = string.Empty;

        // Foreign Key equipo

        public Guid EquipoId { get; set; }

        public Equipo Equipo { get; set; }


        // Foreign Key usuario solicitante

        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        // Foreign Key usuario aprobador

        public Guid? AprobadorId { get; set; }

        public Usuario Aprobador { get; set; }




    }
}
