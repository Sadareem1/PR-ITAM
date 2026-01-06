using SistemaITAM.Application.DTOs;

namespace SistemaITAM.Application.Interfaces;

public interface IPdfGenerator
{
    Task<byte[]> GenerarActaAsignacionAsync(AssignmentDto asignacion);
}
