using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IPrestamoEquipoService
    {
        Task<IEnumerable<PrestamoEquipo>> GetAllAsync();
        Task<PrestamoEquipo> GetByIdAsync(int id);
        Task AddAsync(PrestamoEquipo prestamo);
        Task UpdateAsync(PrestamoEquipo prestamo);
        Task DeleteAsync(int id);
    }
}
