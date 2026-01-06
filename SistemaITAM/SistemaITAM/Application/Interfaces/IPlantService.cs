using SistemaITAM.Domain.Entities;

namespace SistemaITAM.Application.Interfaces;

public interface IPlantService
{
    Task<IReadOnlyCollection<Planta>> GetAllAsync();
    Task<Planta?> GetAsync(Guid id);
    Task<Planta> CreateAsync(string nombre);
    Task<Planta> UpdateAsync(Guid id, string nombre, bool activa);
}
