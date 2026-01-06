using SistemaITAM.Domain.Entities;

namespace SistemaITAM.Application.Interfaces;

public interface IAreaService
{
    Task<IReadOnlyCollection<Area>> GetAllAsync();
    Task<Area?> GetAsync(Guid id);
    Task<Area> CreateAsync(string nombre);
    Task<Area> UpdateAsync(Guid id, string nombre, bool activa);
}
