using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Application.DTOs;

public record AssignmentDto(
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

public record AssignmentRequestDto(Guid ActivoId, Guid UsuarioId, Guid AdministradorId);
public record AssignmentResultDto(AssignmentDto Asignacion, byte[] PdfActa);
