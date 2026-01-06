using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Filters;
using SistemaITAM.Domain.Entities;

namespace SistemaITAM.Application.Interfaces;

public interface IServicioActivos
{
    Task<IReadOnlyCollection<ActivoDto>> GetAsync(FiltroActivos filter);
    Task<ActivoDto?> GetByIdAsync(Guid id);
    Task<Activo> CreateAsync(Activo activo);
    Task<Activo?> UpdateEstadoAsync(Guid id, Domain.Enums.EstadoActivo estado);
}
