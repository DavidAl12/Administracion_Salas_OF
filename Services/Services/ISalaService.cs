using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;

namespace Services
{
    public interface ISalaService
    {
        Task<IEnumerable<Sala>> ObtenerTodasAsync();
        Task<Sala> ObtenerPorIdAsync(int id);
        Task CrearAsync(Sala sala);
        Task ActualizarAsync(Sala sala);
        Task EliminarAsync(int id);
    }
}
