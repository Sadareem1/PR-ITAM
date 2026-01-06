using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Application.Filters;

public record FiltroActivos(
    TipoActivo? Tipo = null,
    EstadoActivo? Estado = null,
    Guid? PlantaId = null,
    Guid? AreaId = null,
    string? Search = null);
