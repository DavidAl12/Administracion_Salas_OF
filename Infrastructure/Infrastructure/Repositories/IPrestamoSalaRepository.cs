using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IPrestamoSalaRepository
    {
        Task CrearAsync(PrestamoSala prestamo);
        Task GuardarAsync();

        Task<PrestamoSala> GetByIdAsync(int id);
        Task<IEnumerable<PrestamoSala>> ObtenerTodasAsync();
        Task<IEnumerable<PrestamoSala>> ObtenerPorUsuarioAsync(int usuarioId);
    }
}
