using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Application.DTOs;

public record ActivoDto(
    Guid Id,
    string CodigoPatrimonial,
    string SerialNumber,
    string Marca,
    string Modelo,
    TipoActivo TipoActivo,
    EstadoActivo Estado,
    Guid PlantaId,
    Guid AreaId,
    string Planta,
    string Area,
    string Observaciones,
    DateTime FechaAlta);
