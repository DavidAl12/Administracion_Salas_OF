using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IEquipoService
    {
        Task<IEnumerable<Equipo>> GetAllAsync();
        Task<Equipo> GetByIdAsync(int id);
        Task AddAsync(Equipo equipo);
        Task UpdateAsync(Equipo equipo);
        Task DeleteAsync(int id);
    }
}
