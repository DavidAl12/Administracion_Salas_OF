using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface ISalaService
    {
        Task<IEnumerable<Sala>> GetAllAsync();       // Listar todas
        Task<Sala> GetByIdAsync(int id);            // Obtener por ID
        Task AddAsync(Sala sala);                   // Crear
        Task UpdateAsync(Sala sala);                // Editar
        Task DeleteAsync(int id);                   // Eliminar
    }
}
