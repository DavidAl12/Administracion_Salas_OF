using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IAsesoriaService
    {
        Task<IEnumerable<Asesoria>> GetAllAsync();
        Task<Asesoria> GetByIdAsync(int id);
        Task AddAsync(Asesoria asesoria);
        Task UpdateAsync(Asesoria asesoria);
        Task DeleteAsync(int id);
    }
}
