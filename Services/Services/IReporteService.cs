using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IReporteService
    {
        Task AddAsync(Reporte reporte);
        Task<IEnumerable<Reporte>> GetAllAsync();
        Task<IEnumerable<Reporte>> GetAllIncludingAsync(); // <- Agregado aquí
        Task<Reporte> GetByIdAsync(int id);
        Task UpdateAsync(Reporte reporte);
        Task DeleteAsync(int id);
    }
}
