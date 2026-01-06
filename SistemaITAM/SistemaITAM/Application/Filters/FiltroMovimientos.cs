using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Application.Filters;

public record FiltroMovimientos(
    Guid? ActivoId = null,
    TipoMovimiento? Tipo = null,
    Guid? PlantaId = null,
    Guid? AreaId = null,
    DateTime? Desde = null,
    DateTime? Hasta = null);
