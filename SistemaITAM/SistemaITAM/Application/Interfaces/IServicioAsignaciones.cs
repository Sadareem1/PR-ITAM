using SistemaITAM.Application.DTOs;

namespace SistemaITAM.Application.Interfaces;

public interface IServicioAsignaciones
{
    Task<IReadOnlyCollection<AsignacionDto>> GetAsync();
    Task<ResultadoAsignacionDto> AssignAsync(SolicitudAsignacionDto request);
}
