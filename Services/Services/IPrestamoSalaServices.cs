using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IPrestamoSalaService
    {
        Task<IEnumerable<PrestamoSala>> GetAllAsync();
        Task<PrestamoSala> GetByIdAsync(int id);
        Task AddAsync(PrestamoSala prestamo);
        Task UpdateAsync(PrestamoSala prestamo);
        Task DeleteAsync(int id);
    }
}
