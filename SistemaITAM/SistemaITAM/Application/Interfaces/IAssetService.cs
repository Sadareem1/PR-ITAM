using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Filters;
using SistemaITAM.Domain.Entities;

namespace SistemaITAM.Application.Interfaces;

public interface IAssetService
{
    Task<IReadOnlyCollection<AssetDto>> GetAsync(AssetFilter filter);
    Task<AssetDto?> GetByIdAsync(Guid id);
    Task<Activo> CreateAsync(Activo activo);
    Task<Activo?> UpdateEstadoAsync(Guid id, Domain.Enums.EstadoActivo estado);
}
