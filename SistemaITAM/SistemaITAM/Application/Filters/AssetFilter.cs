using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Application.Filters;

public record AssetFilter(
    TipoActivo? Tipo = null,
    EstadoActivo? Estado = null,
    Guid? PlantaId = null,
    Guid? AreaId = null,
    string? Search = null);
