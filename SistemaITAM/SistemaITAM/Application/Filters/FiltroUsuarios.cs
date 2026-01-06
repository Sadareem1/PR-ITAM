namespace SistemaITAM.Application.Filters;

public record FiltroUsuarios(Guid? PlantaId = null, Guid? AreaId = null, string? Search = null);
