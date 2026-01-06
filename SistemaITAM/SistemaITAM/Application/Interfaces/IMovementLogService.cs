using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Filters;
using SistemaITAM.Domain.Entities;
using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Application.Interfaces;

public interface IMovementLogService
{
    Task<IReadOnlyCollection<MovementLogDto>> GetAsync(MovementFilter filter);
    Task<MovementLog> RegistrarAsync(Guid activoId, TipoMovimiento tipo, string descripcion, ModuloSistema modulo, string planta, string area, Guid? administradorId = null);
}
