namespace SistemaITAM.Application.Filters;

public record UserFilter(Guid? PlantaId = null, Guid? AreaId = null, string? Search = null);
