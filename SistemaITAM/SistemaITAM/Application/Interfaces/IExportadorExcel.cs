using SistemaITAM.Application.DTOs;

namespace SistemaITAM.Application.Interfaces;

public interface IExportadorExcel
{
    Task<byte[]> ExportarActivosAsync(IEnumerable<ActivoDto> activos);
}
