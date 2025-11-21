using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IReporteService
    {
        Task<IEnumerable<Reporte>> GetAllAsync();
        Task<Reporte> GetByIdAsync(int id);
        Task AddAsync(Reporte reporte);
        Task UpdateAsync(Reporte reporte);
        Task DeleteAsync(int id);
    }
}
