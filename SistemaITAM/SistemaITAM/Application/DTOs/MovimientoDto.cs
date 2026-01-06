using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Application.DTOs;

public record MovimientoDto(
    Guid Id,
    Guid ActivoId,
    string Activo,
    TipoMovimiento Tipo,
    string Descripcion,
    DateTime Fecha,
    string Planta,
    string Area,
    ModuloSistema Modulo);
