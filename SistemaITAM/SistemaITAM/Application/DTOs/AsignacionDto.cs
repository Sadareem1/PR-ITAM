using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Application.DTOs;

public record AsignacionDto(
    Guid Id,
    Guid ActivoId,
    Guid UsuarioId,
    Guid AdministradorId,
    DateTime FechaAsignacion,
    EstadoAsignacion Estado,
    string RutaPdfActa,
    string Activo,
    string Usuario,
    string Planta,
    string Area);

public record SolicitudAsignacionDto(Guid ActivoId, Guid UsuarioId, Guid AdministradorId);
public record ResultadoAsignacionDto(AsignacionDto Asignacion, byte[] PdfActa);
