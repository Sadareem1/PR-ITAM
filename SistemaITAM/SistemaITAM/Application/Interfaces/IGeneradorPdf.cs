using SistemaITAM.Application.DTOs;

namespace SistemaITAM.Application.Interfaces;

public interface IGeneradorPdf
{
    Task<byte[]> GenerarActaAsignacionAsync(AsignacionDto asignacion);
}
